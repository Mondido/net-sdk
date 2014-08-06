using System;
using System.Collections.Generic;
using MondidoSDK.Utils;
using Newtonsoft.Json;

namespace MondidoSDK.Api
{
    public class Refund : BaseModel
    {
        [JsonProperty(PropertyName = "transaction_id")]
        public int TransactionId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "transaction")]
        public Transaction Transaction { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        public static Refund Create(List<KeyValuePair<string, string>> data)
        {
            return HttpPost("/refunds", data).Result.FromJson<Refund>();
        }

        public static Refund Get(int id)
        {
            return HttpGet("/refunds/" + id).Result.FromJson<Refund>();
        }
    }
}
