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
using RestSharp;
using RestSharp.Authenticators;

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

        [JsonProperty(PropertyName = "api_error")]
        public string ApiError { get; set; }

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

        public static string HttpGet(string url)
        {
            string data = "";
            var client = new RestClient(ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(ApiUsername, ApiPassword);
            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            data = ParseResponse(response);
            return data;
        }

        public static string HttpDelete(string url)
        {
            string data = "";
            var client = new RestClient(ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(ApiUsername, ApiPassword);
            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            data = ParseResponse(response);
            return data;
        }

        public static string HttpPost(string url, List<KeyValuePair<string, string>> postData, HttpMethod method = HttpMethod.POST)
        {
            string data = "";
            var client = new RestClient(ApiBaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(ApiUsername, ApiPassword);
            var meth = Method.POST;
            if (method == HttpMethod.PUT)
            {
                meth = Method.PUT;
            }
            var request = new RestRequest(url, meth);
            request = PropData(postData, request);
            IRestResponse response = client.Execute(request);
            data = ParseResponse(response); 
            return data;
        }

        private static string ErrorObj(List<String> arr)
        {
            return "{\"api_error\":\""+String.Join(" ",arr)+"\"}";
        }

        private static string ParseResponse(IRestResponse response)
        {
            foreach(var header in response.Headers)
            {
                if(header.Name == "Status")
                {
                    var headVal = header.Value.ToString().Split(' ');
                    var code = int.Parse(headVal[0]);
                    if(code < 200 || code > 299)
                    {
                        List<string> info = new List<string>(headVal);
                        dynamic error = JsonConvert.DeserializeObject(response.Content);
                        if (error.name != null)
                        {
                            info.Add(error.name.ToString());
                        }
                        return ErrorObj(info);
                    }
                }
            }
            return response.Content;
        }

        private static RestRequest PropData(List<KeyValuePair<string, string>> postData, RestRequest request)
        {
            foreach(var item in postData)
            {
                request.AddParameter(item.Key, item.Value);
            }
            return request;
        }
        public static string HttpPut(string url, List<KeyValuePair<string, string>> postData)
        {
            return  HttpPost(url, postData, HttpMethod.PUT);
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
