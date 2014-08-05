
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MondidoSDK.Utils
{
    public static class Extensions
    {
        public static T FromJson<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}
