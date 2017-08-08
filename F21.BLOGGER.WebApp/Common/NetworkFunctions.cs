using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace F21.BLOGGER.WebApp.Common
{

    public class NetworkFunctions
    {
        public string endPoint = Settings.API_RequestUrl;
        public string apiKey = Settings.API_Key;
        public string countryCode = Settings.CountryCode;
        public enum CallType
        {
            GET,
            POST
        }

        public NetworkFunctions()
        {

        }

        public string GetWebResponseData(string url, CallType callType)
        {
            return GetWebResponseData(url, callType, null);
        }

        public string GetWebResponseData(string url, CallType callType, string requestData)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string localeValue = string.Empty;
            string responseString = string.Empty;
            HttpWebRequest HttpWebReq;
            Uri URI = new Uri(endPoint + url, UriKind.Absolute);
            Stream postStream = null;
            HttpWebResponse webResponse = null;

            HttpWebReq = (HttpWebRequest)WebRequest.Create(URI);

            switch (callType)
            {
                case CallType.GET:
                    HttpWebReq.Method = "GET";
                    break;

                case CallType.POST:
                    HttpWebReq.Method = "POST";
                    break;

                default:
                    HttpWebReq.Method = "GET";
                    break;
            }

            localeValue = "en-US";

            HttpWebReq.Headers.Add("apikey: " + apiKey);
            HttpWebReq.Headers.Add("country: " + countryCode);
            HttpWebReq.Headers.Add("locale: " + localeValue);
            HttpWebReq.Headers.Add("device: web-Forever21HK-none-none");


            if (callType == CallType.POST && requestData != null)
            {
                HttpWebReq.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(HttpWebReq.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            try
            {
                webResponse = (HttpWebResponse)HttpWebReq.GetResponse();

                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();

                    if ((postStream != null))
                        postStream.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return responseString;
        }
    }
}
