using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.Expressions;
using Newtonsoft.Json;

namespace MondidoSDK.Api
{
    public class StoredCard : BaseModel
    {

        [JsonProperty(PropertyName = "card_holder")]
        public string CardHolder { get; set; }

        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber { get; set; }

        [JsonProperty(PropertyName = "card_type")]
        public string CardType { get; set; }

        [JsonProperty(PropertyName = "card_cvv")]
        public string CardCVV { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }


        
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public dynamic Customer { get; set; }

        [JsonProperty(PropertyName = "customer_ref")]
        public string CustomerRef { get; set; }

        [JsonProperty(PropertyName = "customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "encrypted")]
        public string Encrypted { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool test { get; set; }
    }
}