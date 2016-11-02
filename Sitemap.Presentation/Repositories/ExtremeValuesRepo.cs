using System.Linq;
using Sitemap.Presentation.Interfaces;
using Sitemap.Presentation.Models.SitemapData;
using Sitemap.Presentation.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;


namespace Sitemap.Presentation.Repositories
{
    public class ExtremeValuesRepo: IExtremeValues<ExtremeValues>
    {
        private ApplicationContext db;

        public ExtremeValuesRepo(ApplicationContext context)
        {
            this.db = context;
        }
        public void Update(ExtremeValues item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Create (ExtremeValues item)
        {
            db.ExtremeValues.Add(item);
        }
    }
}