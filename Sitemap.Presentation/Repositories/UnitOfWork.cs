using System;
using Sitemap.Presentation.Interfaces;
using Sitemap.Presentation.Models;
using Sitemap.Presentation.Models.SitemapData;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db = new ApplicationContext();
        private IHistoryRepo<RequestHistory> hitoryRepository;
        private ISavedUrlRepo<SavedUrl> savedUrlRepository;
        private IResponseTimeRepo<ResponseTime> responseTimeRepository;

        public IHistoryRepo<RequestHistory> Histories
        {
            get
            {
                if (hitoryRepository == null)
                {
                    hitoryRepository = new HistoryRepo(db);
                }
                return hitoryRepository;
            }
        }

        public ISavedUrlRepo<SavedUrl> SavedUrls
        {
            get
            {
                if (savedUrlRepository == null)
                {
                    savedUrlRepository = new SavedUrlRepo(db);
                }
                return savedUrlRepository;
            }
        }

        public IResponseTimeRepo<ResponseTime> ResponseTime
        {
            get
            {
                if (responseTimeRepository == null)
                {
                    responseTimeRepository = new ResponseTimeRepo(db);
                }
                return responseTimeRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}