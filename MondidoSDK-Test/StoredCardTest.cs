using System;
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



        [TestMethod]
        public void TestMethod1()
        {
            var transaction = Transaction.Get(1);
            Assert.AreEqual(1, transaction.Id);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestBase.MyClassCleanup();
        }
    }
}
