using System;
using System.Collections.Generic;
using System.Linq;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Repositories;
using System.Threading.Tasks;
using Sitemap.Presentation.Interfaces;
using Ninject;

namespace Sitemap.Presentation.Services
{
    class StartResponseEvaluator
    {
        IUnitOfWork uow;
        public StartResponseEvaluator()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            uow = ninjectKernel.Get<IUnitOfWork>();
        }
        //[Inject]
        //public IUnitOfWork uow { private get; set; }

        public async Task<string> StartEvaluation(string url, string userId, int notMoreThan = int.MaxValue)
        {
            FindSitemap findSitemap = new FindSitemap();//use ninject
            PageReaderService pageReader = new PageReaderService();
            List<string> foundSitemap = await findSitemap.SitemapsFinder(url);
            RequestHistory history = await CreateHistoryAsync(url, userId);
            if (foundSitemap == null)
            {
                await pageReader.LinksSearchOnPage(url, notMoreThan, history.ReqestHistoryId, userId);
            }
            else
            {
                for (int i = 0; i < foundSitemap.Count; i++)
                {
                    await pageReader.LinksSearchOnSitemap(foundSitemap[i], history.ReqestHistoryId, userId);
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