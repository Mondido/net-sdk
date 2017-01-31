using Mondido.Request;
using System.Web.Http;

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
