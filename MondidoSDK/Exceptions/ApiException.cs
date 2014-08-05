using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondidoSDK.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string data):  base(data)
        {
        }
    }
}
