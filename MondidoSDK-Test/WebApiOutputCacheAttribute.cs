using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MondidoSDK_Test
{
    public class WebApiOutputCacheAttribute : ActionFilterAttribute
    {
        // client cache length in seconds
        private int _clientTimeSpan;

        public WebApiOutputCacheAttribute(int clientTimeSpan)
        {
            _clientTimeSpan = clientTimeSpan;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var cachecontrol = new CacheControlHeaderValue();
            cachecontrol.MaxAge = TimeSpan.FromSeconds(_clientTimeSpan);
            cachecontrol.MustRevalidate = true;
            actionExecutedContext.ActionContext.Response.Headers.CacheControl = cachecontrol;


        }

    }
}
