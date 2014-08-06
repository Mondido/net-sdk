using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using MondidoSDK.Configuration;
using MondidoSDK.Utils.Security;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace MondidoSDK.Utils
{
    public static class Extensions
    {
        public static T FromJson<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static string ToMD5(this string s)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string RSAEncrypt(this string s)
        {
            const int keySize = 2048;
            var key = AsymmetricEncryption.PemToXml(Settings.RSAKey);
            var base64Card = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
            return AsymmetricEncryption.EncryptText(base64Card, keySize, key);
        }

    }
}