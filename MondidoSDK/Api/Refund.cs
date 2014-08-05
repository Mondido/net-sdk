using System;
using Newtonsoft.Json;

namespace MondidoSDK.Api
{
    public class Refund
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
    }
}
