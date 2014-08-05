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
            var credentials = new NetworkCredential(ApiUsername, ApiPassword);
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync(ApiBaseUrl+url);
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
            var credentials = new NetworkCredential(ApiUsername, ApiPassword);
            var handler = new HttpClientHandler { Credentials = credentials };
            using (var client = new HttpClient(handler))
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


      
    
    }
}
