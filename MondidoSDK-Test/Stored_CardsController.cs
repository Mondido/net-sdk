using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mondido.Payment;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Net.Http;

namespace MondidoSDK_Test
{
    public class Stored_CardsController : ApiController
    {
        public StoredCard TestCard
        {
            get
            {
                return new StoredCard()
                {
                    Id = 1,
                    CardCVV = "200",
                    CardHolder = ".net sdk",
                    CardNumber = "411111******1111",
                    CardType = "VISA",
                    CreatedAt = DateTime.UtcNow,
                    Currency = "eur",
                    Status = "complete",
                    Test = true
                };
            }
        }
        public object Post()
        {
            dynamic obj =  Request.Content.ReadAsAsync<JObject>().Result;
            var sc = TestCard;
            sc.CardHolder = obj.card_holder;
            sc.CardCVV = obj.card_cvv;

            return sc;
        }

        public object Get()
        {
            var limit = HttpUtility.ParseQueryString(this.Request.RequestUri.Query).Get("limit");
            var offset = HttpUtility.ParseQueryString(this.Request.RequestUri.Query).Get("offset");

            if (limit.Any() && offset.Any())
            {
                var list = new List<StoredCard>();
                for (var i = 0; i < int.Parse(limit); i++)
                {
                    list.Add(TestCard);
                }
                return list;
            }
            return null;
        }

        public object Get(int id)
        {
            var trans = TestCard;
            trans.Id = id;
            return trans;
        }

        public object Delete(int id)
        {
            var trans = TestCard;
            trans.Status = "deleted";
            return trans;
        }
    }
}
