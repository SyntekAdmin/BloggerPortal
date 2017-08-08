using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace F21.BLOGGER.WebApp.Common
{
    public class Settings
    {
        private static readonly Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());

        public static string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash) {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }
        public static Settings Instance
        {
            get { return _instance.Value; }
        }

        public string FilePath
        {
            get { return ConfigurationManager.AppSettings["FilePath"]; }
        }

        public string EncryptionKey
        {
            get { return ConfigurationManager.AppSettings["EncryptionKey"]; }
        }
        public string ListReferrer
        {
            get { return "ListReferrer"; }
        }

        public static string API_Version
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiVersion"];
            }
        }
        
        public static string API_Key
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiKey"];
            }
        }

        public static string API_RequestUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiRequestUrl"];
            }
        }

        public static string CountryCode
        {
            get
            {
                return ConfigurationManager.AppSettings["CountryCode"];
            }
        }
    }
}