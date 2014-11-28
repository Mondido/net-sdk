using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Configuration;
using Mondido.CreditCard;
using Mondido.Utils;

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

        [TestInitialize()]
        public void Init()
        {
            TestBase.MyClassInitialize(null);
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
            var transactions = Transaction.List(3, 0);
            Assert.IsTrue(3 == transactions.Count());
        }

        [TestMethod]
        public void TestCreate()
        {
            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            var encryptedCard = "4111111111111111".RSAEncrypt(); //DO NOT SEND/RECEIVE CARD NUMBERS IN CLEAR TEXT

            postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
            postData.Add(new KeyValuePair<string, string>("test", "true"));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", "sek"));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + "10.00" + "sek" + Settings.ApiSecret).ToMD5()));
            postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));

            var transaction = Transaction.Create(postData);
            Assert.IsTrue(transaction.PaymentRef == payment_ref);
        }

        [TestMethod]
        public void TestPrepare()
        {
            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("test", "true"));
            postData.Add(new KeyValuePair<string, string>("currency", "sek"));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("process", "false"));
            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + "10.00" + "sek" + Settings.ApiSecret).ToMD5()));

            var transaction = Transaction.Create(postData);
            Assert.IsNotNull(transaction.Href);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }

    }
}