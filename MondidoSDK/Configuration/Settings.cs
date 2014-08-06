using System.Configuration;

namespace MondidoSDK.Configuration
{
    public class Settings
    {
        public static string ApiBaseUrl
        {
            get
            {
                return ConfigurationSettings.AppSettings["ApiBaseUrl"];
            }
        }

        public static string ApiUsername
        {
            get
            {
                return ConfigurationSettings.AppSettings["ApiUsername"];
            }
        }

        public static string ApiPassword
        {
            get
            {
                return ConfigurationSettings.AppSettings["ApiPassword"];
            }
        }

        public static string ApiSecret
        {
            get
            {
                return ConfigurationSettings.AppSettings["ApiSecret"];
            }
        }

        public static string RSAKey
        {
            get
            {
                return ConfigurationSettings.AppSettings["RSAKey"];
            }
        }    

    }
}
