using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Mondido.Configuration;
using Mondido.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace Mondido.Payment
{
    public enum HttpMethod
    {
        POST,
        PUT
    }

    [DataContract]
    public class BaseModel
    {
        private static string _apiBaseUrl = "";
        private static string _apiUsername = "";
        private static string _apiPassword = "";

        public static string ApiBaseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiBaseUrl))
                {
                    _apiBaseUrl = Settings.ApiBaseUrl;
                }
                return _apiBaseUrl;
            }
        }


        public static string ApiUsername
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUsername))
                {
                    _apiUsername = Settings.ApiUsername;
                }
                return _apiUsername;
            }
        }

        public static string ApiPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_apiPassword))
                {
                    _apiPassword = Settings.ApiPassword;
                }
                return _apiPassword;
            }
        }

        public static async Task<string> HttpGet(string url)
        {
            string data;
            using (var client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                HttpResponseMessage response = await client.GetAsync(ApiBaseUrl+url);
                data = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException(data);
                }
            }
            return data;
        }

        public static async Task<string> HttpDelete(string url)
        {
            string data;
            using (var client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                HttpResponseMessage response = await client.DeleteAsync(ApiBaseUrl + url);
                data = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException(data);
                }
            }
            return data;
        }

        public static async Task<string> HttpPost(string url, List<KeyValuePair<string, string>> postData, HttpMethod method = HttpMethod.POST)
        {
            string data;
            using (var client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(ApiBaseUrl);

                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = null;
                if (method == HttpMethod.POST)
                {
                    response = await client.PostAsync(ApiBaseUrl + url, content);
                }
                if (method == HttpMethod.PUT)
                {
                    response = await client.PutAsync(ApiBaseUrl + url, content);
                }

                data = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException(data);
                }
            }
            return data;
        }

        public static async Task<string> HttpPut(string url, List<KeyValuePair<string, string>> postData)
        {
            return await HttpPost(url, postData, HttpMethod.PUT);
        }

        public static HttpMessageHandler GetHandler()
        {
            if (MessageHandlerProvider.Handler == null)
            {
                var credentials = new NetworkCredential(ApiUsername, ApiPassword);
                var handler = new HttpClientHandler { Credentials = credentials };
                return handler;
            }
            return MessageHandlerProvider.Handler;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        protected static string ParseFilters(List<KeyValuePair<string, string>> filters)
        {
            if (filters == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            filters.ForEach(delegate (KeyValuePair<string, string> kvp)
            {
                sb.Append(string.Format("&filter[{0}]={1}", kvp.Key, kvp.Value));
            });

            return sb.ToString();
        }


    }
}
