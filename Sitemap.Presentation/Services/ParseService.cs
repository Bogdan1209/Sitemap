﻿using AngleSharp.Parser.Html;
using AngleSharp.Parser.Xml;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Sitemap.Presentation.Models.SitemapData;

namespace Sitemap.Presentation.Services
{
    class ParseService
    {
        public async Task<List<string>> FindUrlsAsync(List<string> urls, int notMore)
        {
            for (int i = 0; i <= urls.Count - 1; i++)
            {
                try
                {
                    
                    string path = await DownloadDocumentAsync(urls[i]);
                    List<string> links = HtmlLinksParser(path);//find links on page
                    Standardize(links, urls[i], urls, urls.Count, notMore);
                    if (urls.Count >= notMore)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    urls.Remove(urls[i]);
                    i--;
                }
            }
            return urls;
        }

        public async Task<string> DownloadDocumentAsync(string address)
        {
            string text;
            var client = new WebClient();
            Uri path = new Uri(address);
            try
            {
                text = await client.DownloadStringTaskAsync(path);
                return text;
            }
            catch
            {
                return null;
            }
            finally
            {
                client.Dispose();
            }

        }
        public int GetResponse(string url)// DELETE CONSOLE!!!
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.AllowAutoRedirect = false;
                Stopwatch responseTime = Stopwatch.StartNew();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                responseTime.Stop();
                return (int)responseTime.ElapsedMilliseconds;
            }
            catch (WebException webExcp) //CONSOLE OUTPUT!!! FIX IT!!!
            {
                HttpWebResponse httpResponse = (HttpWebResponse)webExcp.Response;
                WebExceptionStatus status = webExcp.Status;
                if ((int)httpResponse.StatusCode == 403)
                {
                    Console.WriteLine("Sorry, but this site blocking parser. Try another one");
                }
                else if ((int)httpResponse.StatusCode == 404)
                {
                    Console.WriteLine("Sorry, but this page not found. Try another one");
                }
                else
                {
                    Console.WriteLine("A WebException has been caught.");
                    Console.WriteLine(webExcp.ToString());
                    if (status == WebExceptionStatus.ProtocolError)
                    {
                        Console.Write("The server returned protocol error ");
                        Console.WriteLine(httpResponse.StatusCode + " - "
                           + httpResponse.StatusCode);
                    }
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public List<string> HtmlLinksParser(string html)
        {
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            var links = document.QuerySelectorAll("a");
            for (int i = 0; i < links.Length; i++)
            {
                if (links[i].GetAttribute("href") != null && !hrefTags.Contains(links[i].GetAttribute("href")))
                {
                    hrefTags.Add(links[i].GetAttribute("href"));
                }

            }
            return (hrefTags);
        }
        public List<string> XmlLinksParser(string xml)
        {
            try
            {
                List<string> hrefTags = new List<string>();
                var parser = new XmlParser();
                var document = parser.Parse(xml);
                var links = document.QuerySelectorAll("loc");
                for (int i = 0; i < links.Length; i++) 
                {
                    if (links[i].TextContent != null && !hrefTags.Contains(links[i].TextContent))
                    {
                        hrefTags.Add(links[i].TextContent);
                    }

                }
                return (hrefTags);
            }
            catch
            {
                return null;
            }
        }
        public void Standardize(List<string> pageLinks, string pagePath, List<string> urls, int numberUrls, int notMore)
        {
            var uri = new Uri(pagePath);
            string scheme = uri.Scheme + "://";
            string host = uri.Host;
            string domain = scheme + host + "/";
            string url;
            RegexPatternService regexPattern = new RegexPatternService();
            for (int i = 0; i < pageLinks.Count; i++)
            {
                url = pageLinks[i];
                if (regexPattern.CheckReqOrAnc(url))
                {
                    continue;
                }
                url = regexPattern.RelativePath(url, pagePath);
                url = regexPattern.RemoveDoubleBSlash(url);
                url = regexPattern.RootRelativePathToAbsolute(url, domain);
                url = regexPattern.CheckHttp(url, scheme);
                if (!regexPattern.CheckDomain(url, domain))
                {
                    continue;
                }

                if (!urls.Contains(url))
                {
                    urls.Add(url);
                }
                if(urls.Count >= notMore)
                {
                    break;
                }

            }
        }
    }
}