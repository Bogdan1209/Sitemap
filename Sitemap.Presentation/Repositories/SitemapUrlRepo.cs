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
        public void CreateList(List<SavedUrl> urls)
        {
            db.SavedUrls.AddRange(urls);
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

        public async Task<List<UrlViewModel>> GetRespondTimeAsync(int historyId, string userId)
        {
            List<UrlViewModel> evaluatedUrl = await (from history in db.RequestHistories
                                join responseTime in db.ResponseTime on history.ReqestHistoryId equals responseTime.RequestHistoryId
                                where history.ReqestHistoryId == historyId
                                join savedUrl in db.SavedUrls on responseTime.UrlId equals savedUrl.Id
                                join extremeValues in db.ExtremeValues on savedUrl.Id equals extremeValues.UrlId
                                where extremeValues.UserId == userId
                                orderby extremeValues.MinValue
                                select new UrlViewModel
                                {
                                    Url = savedUrl.Url,
                                    MinValue = extremeValues.MinValue,
                                    MaxValue = extremeValues.MaxValue,
                                    CurrentValue = responseTime.TimeOfResponse
                                }).ToListAsync();

            return evaluatedUrl;
        }
        public async Task<List<UrlValueModel>> GetUrlValueAsync (string host, string userId)
        {
            List<UrlValueModel> urls = await (from url in db.SavedUrls
                                              join extremVal in db.ExtremeValues on url.Id equals extremVal.UrlId
                                              where url.Url.Contains(host) || extremVal.UserId == userId
                                              select new UrlValueModel
                                              {
                                                  Url = url.Url,
                                                  MinValue = extremVal.MinValue,
                                                  MaxValue = extremVal.MaxValue,
                                                  ValueId = extremVal.Id,
                                                  UrlId = url.Id,
                                                  UserId = userId
                                              }).ToListAsync();
            return urls;


        }
    }
}