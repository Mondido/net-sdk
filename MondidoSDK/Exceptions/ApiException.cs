using System;

namespace Mondido.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string data):  base(data)
        {
        }
    }
}
