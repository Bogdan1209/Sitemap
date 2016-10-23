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
    class PageReaderService : ParseService
    {
        UnitOfWork uow = new UnitOfWork();//use ninject
        List<string> urls = new List<string>();
        List<string> readed = new List<string>();

        public async Task LinksSearchOnSitemap(string path, int historyId)
        {
            Uri domain = new Uri(path);
            string xmlFile = await DownloadDocumentAsync(path);
            List<string> sitemapLinks = XmlLinksParser(xmlFile);
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
                using (HttpWebResponse httpWebResponse = GetResponse(sitemapLinks[i]))
                {
                    responseTime.Stop();
                    if (httpWebResponse == null)
                    {
                        continue;
                    }
                }
                
                SavedUrl currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == sitemapLinks[i]);
                if (currentUrl == null)
                {
                    currentUrl = new SavedUrl { Url = sitemapLinks[i], MaxValue = (int)responseTime.ElapsedMilliseconds,
                        MinValue = (int)responseTime.ElapsedMilliseconds };
                    uow.SavedUrls.Create(currentUrl);
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

        public async Task LinksSearchOnPage(string url, int notMore, int historyId)
        {
            urls.Add(url);
            for (int i = 0; i <= urls.Count - 1; i++)
            {
                try
                {
                    string path = await DownloadDocumentAsync(urls[i]);
                    List<string> links = HtmlLinksParser(path);//find links on page
                    Standardize(links, urls[i], urls);
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