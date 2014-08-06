using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Mondido.CreditCard;
using Mondido.Request;
using Newtonsoft.Json.Linq;

namespace MondidoSDK_Test
{
    public class WebhookController : ApiController
    {
        public object Post()
        {
            var transaction = Webhook.GetWebhook(this.ControllerContext.Request);
            return transaction;
        }
    }
}
