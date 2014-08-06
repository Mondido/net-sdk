using System.Net.Http;

namespace Mondido.Configuration
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
