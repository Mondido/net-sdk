using System.Net.Http;
using System.Web;
using Mondido.CreditCard;
using Mondido.Utils;
using Newtonsoft.Json.Linq;

namespace Mondido.Request
{
    public class Webhook
    {
        public static Transaction GetWebhook(HttpRequestMessage request)
        {
            var transaction = request.Content.ReadAsStringAsync().Result.FromJson<Transaction>();
            return transaction;
        }
    }
}
