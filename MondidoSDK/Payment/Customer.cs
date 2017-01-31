using System;
using System.Collections.Generic;
using Mondido.Utils;
using Newtonsoft.Json;

namespace Mondido.Payment
{
    public class Customer : BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public string IntervalUnit { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public dynamic Metadata { get; set; }
        

        public static Customer Create(List<KeyValuePair<string, string>> data)
        {
            return HttpPost("/customers", data).Result.FromJson<Customer>();
        }

        public static Customer Get(int id)
        {
            return HttpGet("/customers/" + id).Result.FromJson<Customer>();
        }

        public static IEnumerable<Customer> List(int take, int skip, List<KeyValuePair<string, string>> filters = null, string sortBy = "id:desc")
        {
            string parsedFilters = ParseFilters(filters);
            return HttpGet(string.Format("/customers?limit={0}&offset={1}{3}&order_by={2}", take, skip, sortBy, parsedFilters)).Result.FromJson<IEnumerable<Customer>>();
        }

        public static Customer Update(int id, List<KeyValuePair<string, string>> data)
        {
            return HttpPut(string.Format("/customers/{0}", id), data).Result.FromJson<Customer>();
        }

    }
}