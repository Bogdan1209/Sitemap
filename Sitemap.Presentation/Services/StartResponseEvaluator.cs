﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Repositories;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Services
{
    class StartResponseEvaluator
    {
        UnitOfWork uow;
        public StartResponseEvaluator()//us ninject
        {
            uow = new UnitOfWork();
        }

        public async Task<string> StartEvaluation(string url, string userId, int notMoreThan = int.MaxValue)
        {
            FindSitemap findSitemap = new FindSitemap();//use ninject
            PageReaderService pageReader = new PageReaderService();
            List<string> foundSitemap = await findSitemap.SitemapsFinder(url);
            RequestHistory history = await CreateHistoryAsync(url, userId);
            if (foundSitemap == null)
            {
                await pageReader.LinksSearchOnPage(url, notMoreThan, history.ReqestHistoryId);
            }
            else
            {
                for (int i = 0; i < foundSitemap.Count; i++)
                {
                    await pageReader.LinksSearchOnSitemap(foundSitemap[i], history.ReqestHistoryId);
                }
            }
            history.AverageTime = uow.ResponseTime.GetAverage(history.ReqestHistoryId);
            uow.Histories.Update(history);
            await uow.SaveAsync();
            uow.Dispose();
            return ("Complete");
        }
        private async Task<RequestHistory> CreateHistoryAsync(string path, string userId)
        {
            var domain = new Uri(path);
            RequestHistory newHistory = new RequestHistory { UserId = userId, TimeOfStart = DateTime.Now, SiteDomain = domain.Host };
            uow.Histories.Create(newHistory);
            await uow.SaveAsync();
            return newHistory;
        }
    }
}