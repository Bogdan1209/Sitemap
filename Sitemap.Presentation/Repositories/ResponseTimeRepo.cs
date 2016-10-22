using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitemap.Presentation.Interfaces;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sitemap.Presentation.Repositories
{
    public class ResponseTimeRepo: IResponseTimeRepo<ResponseTime>
    {
        private ApplicationContext db;

        public ResponseTimeRepo(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create (ResponseTime item)
        {
            db.ResponseTime.Add(item);
        }

        public double GetAverage(int historyId)
        {
            double meanValue = db.ResponseTime
                .Where(x => x.RequestHistoryId == historyId)
                .Select(x => x.TimeOfResponse)
                .Average();
            meanValue =  Math.Round(meanValue, 2);
            return meanValue;
        }

        public async Task<List<ResponseTime>> GetAsync (int historyId)
        {
            IQueryable<ResponseTime> responses = db.ResponseTime.Where(r => r.RequestHistoryId == historyId);
            return await responses.ToListAsync();
        }
        public async void DeleteAsync(int id)
        {
            ResponseTime response = await db.ResponseTime.FindAsync(id);
            if (response != null)
            {
                db.ResponseTime.Remove(response);
            }
        }
    }
}