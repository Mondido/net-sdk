using System;
using System.Security.Cryptography;
using System.Text;
using Mondido.Configuration;
using Mondido.Utils.Security;
using Newtonsoft.Json;

namespace Mondido.Utils
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