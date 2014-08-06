using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MondidoSDK.Api;
using MondidoSDK.Configuration;
using MondidoSDK.Utils;

namespace MondidoSDK_Test
{
    [TestClass]
    public class TransactionTests : TestBase
    {
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestBase.MyClassInitialize(testContext);
        }


        [TestMethod]
        public void TestGet()
        {
            var transaction = Transaction.Get(1);
            Assert.AreEqual(1, transaction.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var transactions = MondidoSDK.Api.Transaction.List(3,0);
            Assert.IsTrue(3 == transactions.Count());
        }

        [TestMethod]
        public void TestCreate()
        {
            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
            postData.Add(new KeyValuePair<string, string>("test", "true"));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", "4012888888881881"));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", "sek"));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + "10.00" + "sek" + Settings.ApiSecret).ToMD5()));


            var transaction = MondidoSDK.Api.Transaction.Create(postData);
            Assert.IsTrue(transaction.PaymentRef == payment_ref);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }

    }
}
