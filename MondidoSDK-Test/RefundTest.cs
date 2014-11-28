using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Mondido.CreditCard;

namespace MondidoSDK_Test
{
    [TestClass]
    public class RefundTest : TestBase
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
            var res = Refund.Get(1);

            Trace.Write(res.ToJson());
            Assert.AreEqual(1, res.Id);
        }

        [TestMethod]
        public void TestCreate()
        {
            var refdata = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("transaction_id", "1"));
            postData.Add(new KeyValuePair<string, string>("amount", "1.00"));
            postData.Add(new KeyValuePair<string, string>("reason", refdata));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));

            var res = Refund.Create(postData);

            Trace.Write(res.ToJson());

            Assert.IsTrue(res.Reason == refdata);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }
    }
}
