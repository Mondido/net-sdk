using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MondidoSDK.Api;
using MondidoSDK.Configuration;
using MondidoSDK.Utils;

namespace MondidoSDK_Test
{
    [TestClass]
    public class StoredCardTest : TestBase
    {

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
           TestBase.MyClassInitialize(testContext);
        }

        [TestInitialize()]
        public void Init()
        {
            TestBase.MyClassInitialize(null);
        }

        [TestMethod]
        public void TestGet()
        {
            var res = StoredCard.Get(1);
            Assert.AreEqual(1, res.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var res = StoredCard.List(3,0);
            Assert.AreEqual(3, res.Count());
        }

        [TestMethod]
        public void TestCreate()
        {
            var refdata = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", refdata));
            postData.Add(new KeyValuePair<string, string>("test", "true"));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", "4012888888881881"));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", "sek"));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));

            var res = MondidoSDK.Api.StoredCard.Create(postData);
            Assert.IsTrue(res.CardHolder == refdata);
        }

        [TestMethod]
        public void TestDelete()
        {
            var res = StoredCard.Delete(1);
            Assert.AreEqual("deleted", res.Status);
        }


        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }
    }
}
