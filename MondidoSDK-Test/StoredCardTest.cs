using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mondido.Payment;
using Mondido.Utils;

namespace MondidoSDK_Test
{
    [TestClass]
    public class StoredCardTest : TestBase
    {

        [TestInitialize()]
        public void Init()
        {
            TestBase.MyClassInitialize(null);
        }

        [TestMethod]
        public void TestGet()
        {
            var res = StoredCard.Get(1);

            Trace.Write(res.ToJson());
            Assert.AreEqual(1, res.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var res = StoredCard.List(3,0);

            Trace.Write(res.ToJson());
            Assert.AreEqual(3, res.Count());
        }

        [TestMethod]
        public void TestCreate()
        {
            var refdata = DateTimeOffset.Now.Ticks.ToString();
            var postData = new List<KeyValuePair<string, string>>();
            var encryptedCard = "4111111111111111".RSAEncrypt();

            postData.Add(new KeyValuePair<string, string>("card_expiry", "0116"));
            postData.Add(new KeyValuePair<string, string>("card_holder", refdata));
            postData.Add(new KeyValuePair<string, string>("test", "true"));
            postData.Add(new KeyValuePair<string, string>("card_cvv", "200"));
            postData.Add(new KeyValuePair<string, string>("card_number", encryptedCard));
            postData.Add(new KeyValuePair<string, string>("card_type", "VISA"));
            postData.Add(new KeyValuePair<string, string>("currency", "sek"));
            postData.Add(new KeyValuePair<string, string>("locale", "en"));
            postData.Add(new KeyValuePair<string, string>("encrypted", "card_number"));

            var res = StoredCard.Create(postData);

            Trace.Write(res.ToJson());
            Assert.IsTrue(res.CardHolder == refdata);
        }

        [TestMethod]
        public void TestDelete()
        {
            var res = StoredCard.Delete(1);

            Trace.Write(res.ToJson());

            Assert.AreEqual("deleted", res.Status);
        }


        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }
    }
}
