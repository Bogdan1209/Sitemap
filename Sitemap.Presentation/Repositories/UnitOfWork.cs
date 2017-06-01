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
        private IExtremeValues<ExtremeValues> extremeValuesRepositiry;
        private SqlInjectionRepo sqlInjection;
        private DosAttackRepo dosAttack;

        public SqlInjectionRepo SqlInjection
        {
            get
            {
                if (sqlInjection == null)
                {
                    sqlInjection = new SqlInjectionRepo(db);
                }
                return sqlInjection;
            }
        }

        public DosAttackRepo DosAttack
        {
            get
            {
                if (dosAttack == null)
                {
                    dosAttack = new DosAttackRepo(db);
                }
                return dosAttack;
            }
        }

        public IExtremeValues<ExtremeValues> ExtremeValues
        {
            get
            {
                if(extremeValuesRepositiry == null)
                {
                    extremeValuesRepositiry = new ExtremeValuesRepo(db);
                }
                return extremeValuesRepositiry;
            }
        }

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