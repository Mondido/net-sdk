using System.Configuration;

namespace Mondido.Configuration
{
    public class Settings
    {
        public static string ApiBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiBaseUrl"];
            }
        }

        public static string ApiUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiUsername"];
            }
        }

        public static string ApiPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiPassword"];
            }
        }

        public static string ApiSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiSecret"];
            }
        }

        public static string RSAKey
        {
            get
            {
                return ConfigurationManager.AppSettings["RSAKey"];
            }
        }    

    }
}
