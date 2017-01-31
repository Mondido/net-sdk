using System.Net.Http;
using Mondido.Payment;
using Mondido.Utils;

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
