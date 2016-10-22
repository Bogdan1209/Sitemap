using System.Collections.Generic;
using System.Linq;
using Sitemap.Presentation.Interfaces;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sitemap.Presentation.Repositories
{
    public class SavedUrlRepo : ISavedUrlRepo<SavedUrl>
    {
        private ApplicationContext db;

        public SavedUrlRepo(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create (SavedUrl item)
        {
            db.SavedUrls.Add(item);
        }

        public async Task<List<SavedUrl>> GetAsync(int id)
        {
            IQueryable<SavedUrl> savedUrls = db.SavedUrls.Where(h => h.Id == id);
            return await savedUrls.ToListAsync();
        }

        public List<SavedUrl> FindByDomain(string domain)
        {
            IQueryable<SavedUrl> foundUrls = db.SavedUrls.Where(h => h.Url.Contains(domain));
            return foundUrls.ToList();
        }

        public async Task<SavedUrl> FindByUrlAsync(string url)
        {
            SavedUrl foundUrl = await db.SavedUrls.FirstOrDefaultAsync(i => i.Url == url);
            return foundUrl;
        }

        public void Update(SavedUrl item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<UrlViewModel>> GetRespondTimeAsync(int historyId)
        {
            List<UrlViewModel> evaluatedUrl = await (from history in db.RequestHistories
                                join responseTime in db.ResponseTime on history.ReqestHistoryId equals responseTime.RequestHistoryId
                                where history.ReqestHistoryId == historyId
                                join savedUrl in db.SavedUrls on responseTime.UrlId equals savedUrl.Id
                                orderby savedUrl.MinValue
                                select new UrlViewModel
                                {
                                    Url = savedUrl.Url,
                                    MinValue = savedUrl.MinValue,
                                    MaxValue = savedUrl.MaxValue,
                                    CurrentValue = responseTime.TimeOfResponse
                                }).ToListAsync();

            return evaluatedUrl;
        }
    }
}