using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Mondido.CreditCard;
using Newtonsoft.Json.Linq;

namespace MondidoSDK_Test
{
    public class TransactionsController : ApiController
    {
        public Transaction TestTransaction
        {
            get
            {
                return new Transaction()
                {
                    Id = 1,
                    Amount = "10.00",
                    CardCVV = "200",
                    CardHolder = ".net sdk",
                    CardNumber = "411111******1111",
                    CardType = "VISA",
                    Cost = new { fixed_fee = "2.5", percentual_exchange_fee = "0.035", percentual_fee = "0.025", total = "2.8"},
                    CreatedAt = DateTime.UtcNow,
                    Currency = "eur",
                    TemplateId = 1,
                    PaymentRef = "123",
                    CustomerRef = "456",
                    Status = "complete",
                    Test = true,
                    Href = "https://pay.mondido.com/v1/form/Wcxn78Ow5EuxsZS4rIdx5w"
                    
                };
            }
        }
        public object Post()
        {
            dynamic obj =  Request.Content.ReadAsAsync<JObject>().Result;
            var transaction = TestTransaction;
            transaction.Amount = obj.amount;
            transaction.PaymentRef = obj.payment_ref;
            transaction.CardCVV = obj.card_cvv;

            return transaction;
        }

        public object Get()
        {
            var limit = HttpUtility.ParseQueryString(this.Request.RequestUri.Query).Get("limit");
            var offset = HttpUtility.ParseQueryString(this.Request.RequestUri.Query).Get("offset");

            if (limit.Any() && offset.Any())
            {
                var list = new List<Transaction>();
                for (var i = 0; i < int.Parse(limit); i++)
                {
                    list.Add(TestTransaction);
                }
                return list;
            }
            return null;
        }

        public object Get(int id)
        {
            var trans = TestTransaction;
            trans.Id = id;
            return trans;
        }
    }
}
