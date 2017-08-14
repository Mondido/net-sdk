using System;
using System.Collections.Generic;
using Mondido.Utils;
using Newtonsoft.Json;

namespace Mondido.Payment
{
    public class Plan : BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty(PropertyName = "periods")]
        public int Periods { get; set; }

        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "setup_fees")]
        public dynamic SetupFees { get; set; }

        [JsonProperty(PropertyName = "interval")]
        public int Interval { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "trial_length")]
        public int TrialLength { get; set; }

        public static Plan Create(List<KeyValuePair<string, string>> data)
        {
            return HttpPost("/plans", data).FromJson<Plan>();
        }

        public static Plan Get(int id)
        {
            return HttpGet("/plans/" + id).FromJson<Plan>();
        }

        public static IEnumerable<Plan> List(int take, int skip, List<KeyValuePair<string, string>> filters = null, string sortBy = "id:desc")
        {
            string parsedFilters = ParseFilters(filters);
            return HttpGet(string.Format("/plans?limit={0}&offset={1}{3}&order_by={2}", take, skip, sortBy, parsedFilters)).FromJson<IEnumerable<Plan>>();
        }

        public static Plan Delete(int id)
        {
            return HttpDelete(string.Format("/plans/" + id)).FromJson<Plan>();
        }

    }
}