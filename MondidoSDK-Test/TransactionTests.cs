using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Configuration;
using Mondido.Payment;
using Mondido.Utils;
using Newtonsoft.Json.Linq;

namespace MondidoSDK_Test
{
    [TestClass]
    public class TransactionTests : TestBase
    {

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
            var customer_ref = "Customer Reference Test";
            var currency = "sek";
            var test = "true";
            var encryptedCard = "4111111111111111".RSAEncrypt(); //DO NOT SEND/RECEIVE CARD NUMBERS IN CLEAR TEXT
            var amount = "10.00";
            var successUrl = "https://www.mondido.com/success/";
            var failUrl = "https://www.mondido.com/fail/";
            var planId = "";
            var webhook = "";


            postData.Add(new KeyValuePair<string, string>("amount", amount));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
            postData.Add(new KeyValuePair<string, string>("test", test));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", currency));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));
            postData.Add(new KeyValuePair<string, string>("customer_ref", customer_ref));
            postData.Add(new KeyValuePair<string, string>("metadata", CreateMetadata().ToString()));
            postData.Add(new KeyValuePair<string, string>("items", CreateItems().ToString()));
            postData.Add(new KeyValuePair<string, string>("authorize", "false"));
            postData.Add(new KeyValuePair<string, string>("success_url", successUrl));
            postData.Add(new KeyValuePair<string, string>("fail_url", failUrl));
            postData.Add(new KeyValuePair<string, string>("plan_id", planId));
            postData.Add(new KeyValuePair<string, string>("webhook", webhook));

            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + customer_ref + amount + currency + (test.Equals("true") ? "test" : "") + Settings.ApiSecret).ToMD5()));

            var transaction = Transaction.Create(postData);
            Assert.IsTrue(transaction.PaymentRef == payment_ref);
        }

//        [TestMethod]
        public void TestCapture()
        {
            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            var customer_ref = "Customer Reference Test";
            var currency = "sek";
            var test = "true";
            var encryptedCard = "4111111111111111".RSAEncrypt(); //DO NOT SEND/RECEIVE CARD NUMBERS IN CLEAR TEXT
            var amount = "10.00";
            var successUrl = "https://www.mondido.com/success/";
            var failUrl = "https://www.mondido.com/fail/";
            var planId = "";
            var webhook = "";

            var authorize = "true"; //means that we want to reserve the amount and capture later

            postData.Add(new KeyValuePair<string, string>("amount", amount));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", ".net sdk"));
            postData.Add(new KeyValuePair<string, string>("test", test));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", currency));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));
            postData.Add(new KeyValuePair<string, string>("customer_ref", customer_ref));
            postData.Add(new KeyValuePair<string, string>("metadata", CreateMetadata().ToString()));
            postData.Add(new KeyValuePair<string, string>("items", CreateItems().ToString()));
            postData.Add(new KeyValuePair<string, string>("authorize", authorize));
            postData.Add(new KeyValuePair<string, string>("success_url", successUrl));
            postData.Add(new KeyValuePair<string, string>("fail_url", failUrl));
            postData.Add(new KeyValuePair<string, string>("plan_id", planId));
            postData.Add(new KeyValuePair<string, string>("webhook", webhook));

            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + customer_ref + amount + currency + (test.Equals("true") ? "test" : "") + Settings.ApiSecret).ToMD5()));

            var transaction = Transaction.Create(postData);
            Assert.IsTrue(transaction.Status == "authorized");

            transaction = Transaction.Capture(transaction.Id, amount);
            Assert.IsTrue(transaction.Status == "approved");
        }
        [TestMethod]
        public void TestPrepare()
        {
            var payment_ref = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            var customer_ref = "Customer Reference Test";
            var currency = "sek";
            var test = "true";

            postData.Add(new KeyValuePair<string, string>("amount", "10.00"));
            postData.Add(new KeyValuePair<string, string>("success_url", "https://www.mondido.com"));
            postData.Add(new KeyValuePair<string, string>("error_url", "https://www.mondido.com"));
            postData.Add(new KeyValuePair<string, string>("payment_ref", payment_ref));
            postData.Add(new KeyValuePair<string, string>("customer_ref", customer_ref));
            postData.Add(new KeyValuePair<string, string>("test", test));
            postData.Add(new KeyValuePair<string, string>("currency", currency));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("process", "false"));
            postData.Add(new KeyValuePair<string, string>("hash", (Settings.ApiUsername + payment_ref + customer_ref + "10.00" + currency + (test.Equals("true") ? "test" : "" ) + Settings.ApiSecret).ToMD5()));

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