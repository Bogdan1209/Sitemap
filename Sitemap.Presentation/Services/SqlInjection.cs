using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitemap.Presentation.ExtentionMethods;
using System.Net;
using System.Text;
using System.IO;

namespace Sitemap.Presentation.Services
{
    public class SqlInjection
    {
        public Dictionary<string, string> ChangeGetParametrs(string url)
        {
            Uri uri = new Uri(url);
            Dictionary<string, string> parametrsOfGet = uri.DecodeGetQueryParameters();
            foreach (var parametr in parametrsOfGet)
            {
                int numbericValueOfRequest;
                if (Int32.TryParse(parametr.Value, out numbericValueOfRequest))
                {
                    parametrsOfGet[parametr.Key] = parametr.Value + "'";
                    return parametrsOfGet;
                }
            }
            return null;
        }

        public string SendGet(Dictionary<string, string> parametrs, string url)
        {
            Uri uri = new Uri(url);
            WebClient webClient = new WebClient();
            foreach (var parametr in parametrs)
            {
                webClient.QueryString.Add(parametr.Key, parametr.Value);
            }
            string address = uri.Scheme + "://" + uri.Host + uri.AbsolutePath;
            string result = webClient.DownloadString(address);
            return result;
        }

        public string SendGet(string url)
        {
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(url);
            return result;
        }

        public List<string> SendPost(string url, string postData)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.CookieContainer = new CookieContainer();
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    List<string> cookies = new List<string>();
                    foreach (var cookie in webResponse.Cookies)
                    {
                        cookies.Add(cookie.ToString());
                    }
                    return cookies;
                }
            }
            catch
            {
                return null;
            }

        }

    }
}