using System;
using Mondido.Payment;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Net.Http;

namespace MondidoSDK_Test
{
    public class RefundsController : ApiController
    {
        public Refund TestRefund
        {
            get
            {
                return new Refund()
                {
                    Amount = "10.00",
                    CreatedAt = DateTime.UtcNow,
                    Reason = "test refund"
                };
            }
        }
        public object Post()
        {
            dynamic obj =  Request.Content.ReadAsAsync<JObject>().Result;
            var re = TestRefund;
            re.Amount = obj.amount;
            re.Reason = obj.reason;
            re.TransactionId = obj.transaction_id;
            re.Id = 100;
            
            return re;
        }

        public object Get(int id)
        {
            var re = TestRefund;
            re.Id = id;
            return re;
        }
    }
}
