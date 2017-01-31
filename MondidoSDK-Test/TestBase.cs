using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Configuration;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http.Routing;

namespace MondidoSDK_Test
{
    [TestClass]
    public class TestBase
    {
        public static System.Web.Http.HttpServer _server;
        internal static string _url = "http://api.mondido.com";

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("capture", "v1/{controller}/{id}/capture",
                defaults: new { id = RouteParameter.Optional });
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

        public static dynamic CreateMetadata()
        {
            dynamic metadata = new JObject();
            metadata.customer = new JObject();
            metadata.products = new JArray();
            dynamic p1 = new JObject();
            dynamic p2 = new JObject();
            p1.name = "Product name";
            p1.price = "5.00";
            p2.name = "Product name2";
            p2.price = "5.00";
            metadata.products.Add(p1);
            metadata.products.Add(p2);
            metadata.customer.name = "Tester";
            metadata.customer.id = "1";
            return metadata;
        }

        public static dynamic CreateItems()
        {
            dynamic items = new JArray();
            dynamic p1 = new JObject();
            dynamic p2 = new JObject();
            p1.artno = Guid.NewGuid();
            p1.description = "An article";
            p1.amount = "5.00";
            p1.vat = "25.00";
            p1.discount = "0.00";
            p1.qty = "1";

            p2.artno = Guid.NewGuid();
            p2.description = "An article";
            p2.amount = "5.00";
            p2.vat = "25.00";
            p2.discount = "0.00";
            p2.qty = "1";

            items.Add(p1);
            items.Add(p2);

            return items;
        }

    }
}
