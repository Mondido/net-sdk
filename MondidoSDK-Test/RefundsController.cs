using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MondidoSDK.Api;
using Newtonsoft.Json.Linq;

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
