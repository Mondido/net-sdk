using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MondidoSDK_Test
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void TestGet()
        {
            var transaction = MondidoSDK.Api.Transaction.Get(443);
            Assert.IsTrue(443==transaction.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var transactions = MondidoSDK.Api.Transaction.List(3,0);
            Assert.IsTrue(3 == transactions.Count());
        }

    }
}
