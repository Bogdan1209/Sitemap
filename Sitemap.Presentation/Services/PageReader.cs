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
            RequestHistory previousHistoriesOfThisDomain = await uow.Histories.FindByDomainAsync(domain.Host);
            if (previousHistoriesOfThisDomain != null)
            {
                urlFromHistory = uow.SavedUrls.FindByDomain(domain.Host);
            }

            for (int i = 0; i < sitemapLinks.Count; i++)
            {
                int responseTime = GetResponse(sitemapLinks[i]);
                if(responseTime == -1)
                {
                    continue;
                }
                
                SavedUrl currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == sitemapLinks[i]);
                //if (currentUrl == null)
                //{
                //    currentUrl = new SavedUrl { Url = sitemapLinks[i], MaxValue = responseTime,
                //        MinValue = responseTime };
                //    uow.SavedUrls.Create(currentUrl);
                //}
                //else
                //{
                //    if(currentUrl.MaxValue < responseTime)
                //    {
                //        currentUrl.MaxValue = responseTime;
                //        uow.SavedUrls.Update(currentUrl);
                //    }
                //    else if (currentUrl.MinValue > responseTime)
                //    {
                //        currentUrl.MinValue = responseTime;
                //        uow.SavedUrls.Update(currentUrl);
                //    }
                //}
                ResponseTime newResponseTime = new ResponseTime { RequestHistoryId = historyId, UrlId = currentUrl.Id,
                    TimeOfResponse = responseTime};
                uow.ResponseTime.Create(newResponseTime);
                await uow.SaveAsync();
                
            }
        }

        public async Task LinksSearchOnPage(string url, int notMore, int historyId)
        {
            Uri domain = new Uri(url);
            List<string> urls = new List<string>();
            urls.Add(url);

            urls = await FindUrlsAsync(urls, notMore);
            //find urls in database
            List<SavedUrl> urlFromHistory = new List<SavedUrl>();
            RequestHistory previousHistoriesOfThisDomain = await uow.Histories.FindByDomainAsync(domain.Host);
            if (previousHistoriesOfThisDomain != null)
            {
                urlFromHistory = uow.SavedUrls.FindByDomain(domain.Host);
            }
            for (int i = 0; i < urls.Count; i++)
            {
                int responseTime = GetResponse(urls[i]);
                if (responseTime == -1)
                {
                    continue;
                }

                SavedUrl currentUrl = urlFromHistory.FirstOrDefault(c => c.Url == urls[i]);
                //if (currentUrl == null)
                //{
                //    currentUrl = new SavedUrl
                //    {
                //        Url = urls[i],
                //        MaxValue = responseTime,
                //        MinValue = responseTime
                //    };
                //    uow.SavedUrls.Create(currentUrl);
                //    await uow.SaveAsync();
                //}
                //else
                //{
                //    if (currentUrl.MaxValue < responseTime)
                //    {
                //        currentUrl.MaxValue = responseTime;
                //        uow.SavedUrls.Update(currentUrl);
                //    }
                //    else if (currentUrl.MinValue > responseTime)
                //    {
                //        currentUrl.MinValue = responseTime;
                //        uow.SavedUrls.Update(currentUrl);
                //    }
                //}
                ResponseTime newResponseTime = new ResponseTime
                {
                    RequestHistoryId = historyId,
                    UrlId = currentUrl.Id,
                    TimeOfResponse = responseTime
                };
                uow.ResponseTime.Create(newResponseTime);
                await uow.SaveAsync();
            }
        }
    }
}