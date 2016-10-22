using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Repositories;

namespace Sitemap.Presentation.Services
{
    class PageReaderService
    {
        UnitOfWork uow = new UnitOfWork();
        List<string> urls = new List<string>();
        List<string> readed = new List<string>();

        //public PageReaderService(string path, int notMore, string userId, int historyId)
        //{
        //    this.urls.Add(path);
        //    LinksSearchOnPage(urls, notMore, historyId);
        //}

        //public PageReaderService(string path, string userId, int historyId)
        //{
        //    LinksSearchOnSitemap(path, historyId);
        //}

        public async Task LinksSearchOnSitemap(string path, int historyId)
        {
            Uri domain = new Uri(path);
            ParseService parserClass = new ParseService();
            string xmlFile = await parserClass.DownloadDocumentAsync(path);
            List<string> sitemapLinks = parserClass.XmlLinksParser(xmlFile);
            if (sitemapLinks == null)
            {
                return;
            }

            List<SavedUrl> urlFromHistory = new List<SavedUrl>();
            RequestHistory beforeHistoriesOfThisDomain = await uow.Histories.FindByDomainAsync(domain.Host);
            if (beforeHistoriesOfThisDomain != null)
            {
                urlFromHistory = uow.SavedUrls.FindByDomain(domain.Host);
            }

            for (int i = 0; i < sitemapLinks.Count; i++)
            {
                Stopwatch responseTime = Stopwatch.StartNew();
                using (HttpWebResponse httpWebResponse = parserClass.GetResponse(sitemapLinks[i]))
                {
                    responseTime.Stop();
                }
                SavedUrl currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == sitemapLinks[i]);
                if (currentUrl == null)
                {
                    currentUrl = new SavedUrl { Url = sitemapLinks[i], MaxValue = (int)responseTime.ElapsedMilliseconds,
                        MinValue = (int)responseTime.ElapsedMilliseconds };
                    uow.SavedUrls.Create(currentUrl);
                    await uow.SaveAsync();
                }
                else
                {
                    if(currentUrl.MaxValue < responseTime.ElapsedMilliseconds)
                    {
                        currentUrl.MaxValue = (int)responseTime.ElapsedMilliseconds;
                        uow.SavedUrls.Update(currentUrl);
                    }
                    else if (currentUrl.MinValue > responseTime.ElapsedMilliseconds)
                    {
                        currentUrl.MinValue = (int)responseTime.ElapsedMilliseconds;
                        uow.SavedUrls.Update(currentUrl);
                    }
                }
                ResponseTime newResponseTime = new ResponseTime { RequestHistoryId = historyId, UrlId = currentUrl.Id,
                    TimeOfResponse = (int)responseTime.ElapsedMilliseconds };
                uow.ResponseTime.Create(newResponseTime);
                await uow.SaveAsync();
            }
        }

        public void LinksSearchOnPage(string url, int notMore, int historyId)
        {
            urls.Add(url);
            ParseService parserClass = new ParseService();
            for (int i = 0; i <= urls.Count - 1; i++)
            {

                try
                {
                    Stopwatch readPage = Stopwatch.StartNew();
                    HttpWebResponse httpWebResponse = parserClass.GetResponse(urls[i]);
                    readPage.Stop();
                    Stream stream = httpWebResponse.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(httpWebResponse.CharacterSet));


                    Stopwatch parse = Stopwatch.StartNew();
                    List<string> links = parserClass.HtmlLinksParser(reader.ReadToEnd());
                    parse.Stop();
                    parserClass.Standardize(links, urls[i], urls);

                    if (i >= notMore)
                    {
                        break;
                    }
                    readed.Add(urls[i]);
                }
                catch (Exception)
                {
                    urls.Remove(urls[i]);
                    i--;
                }
            }
        }
    }
}