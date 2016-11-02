using System.Linq;
using Sitemap.Presentation.Interfaces;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace Sitemap.Presentation.Repositories
{
    public class HistoryRepo : IHistoryRepo<RequestHistory>
    {
        private ApplicationContext db;

        public HistoryRepo(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create (RequestHistory item)
        {
            db.RequestHistories.Add(item);
        }

        public async Task<RequestHistory> FindByIdAsync(int id)
        {
            RequestHistory history = await db.RequestHistories.FindAsync(id);
            return history;
        }

        public async Task<List<RequestHistory>> FindByUserIdAsync (string userId)
        {
            IQueryable<RequestHistory> histories = db.RequestHistories.Where(h => h.UserId == userId);
            return await histories.ToListAsync();
        }

        public async Task<RequestHistory> FindByDomainAsync(string domain)
        {
            RequestHistory histories = await db.RequestHistories.FirstOrDefaultAsync(h => h.SiteDomain == domain);
            return histories;
        }

        public void Update(RequestHistory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            RequestHistory history = await db.RequestHistories.FindAsync(id);
            if(history != null)
            {
                db.RequestHistories.Remove(history);
            }
        }

    }
}