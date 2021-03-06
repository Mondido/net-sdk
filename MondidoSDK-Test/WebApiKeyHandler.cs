﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MondidoSDK_Test
{
    public class WebApiKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            bool isAuth = true;
            if (!isAuth)
            {
                return SendError("You can't use the API without the key.", HttpStatusCode.Forbidden);
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }
        }

        private Task<HttpResponseMessage> SendError(string error, HttpStatusCode code)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(error);
            response.StatusCode = code;
            return Task<HttpResponseMessage>.Factory.StartNew(() => response);
        }

    }

}
