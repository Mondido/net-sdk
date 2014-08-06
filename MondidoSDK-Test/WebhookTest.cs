using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Policy;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Configuration;
using Mondido.CreditCard;
using Mondido.Utils;

namespace MondidoSDK_Test
{
    [TestClass]
    public class WebhookTest
    {
        public static HttpServer _server;
        internal static string _url = "http://api.mondido.com";

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}");
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.Formatters.Insert(0, new JsonNetFormatter());
            _server = new HttpServer(config);
        }


        [TestMethod]
        public void TestWebhook()
        {
            var client = new HttpClient(_server);

            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            var postData = new Transaction();
            postData.Amount = "10.00";
            postData.PaymentRef = payment_ref;

            var request = TestBase.createRequest<Transaction>("/webhook", "application/json", HttpMethod.Post, postData,
                new JsonMediaTypeFormatter());

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                var trans = response.Content.ReadAsAsync<Transaction>().Result;
                Assert.IsNotNull(response.Content);
                Assert.AreEqual(trans.PaymentRef, payment_ref);
            }

            request.Dispose();
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
    }
}