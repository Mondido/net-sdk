using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MondidoSDK_Test
{
    [TestClass]
    public class TestBase
    {
        public static HttpServer _server;
        internal static string _url = "http://api.mondido.com";

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.MessageHandlers.Add(new WebApiKeyHandler());
           config.Formatters.Insert(0, new JsonNetFormatter());
            _server = new HttpServer(config);
            //setting the handler to in memory http server instead of live http handler
            MessageHandlerProvider.Handler = _server;

        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            if (_server != null)
            {
                _server.Dispose();
            }

        }



        public static HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(_url + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        public static HttpRequestMessage createRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = createRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }


    }
}
