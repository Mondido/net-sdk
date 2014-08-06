using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MondidoSDK.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MondidoSDK.Exceptions;
using Newtonsoft.Json;

namespace MondidoSDK.Api
{
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

        public static async Task<string> HttpPost(string url, List<KeyValuePair<string, string>> postData)
        {
            string data;
            using (var client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(ApiBaseUrl);

                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(ApiBaseUrl + url,content);
                data = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException(data);
                }
            }
            return data;
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
      
    
    }
}
