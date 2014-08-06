using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MondidoSDK.Configuration
{
    public class MessageHandlerProvider
    {
        private static HttpMessageHandler _handler = null;

        public static HttpMessageHandler Handler
        {
            get
            {
                return _handler;
            }
            set
            {
                _handler = value;
            }
        }
    }
}
