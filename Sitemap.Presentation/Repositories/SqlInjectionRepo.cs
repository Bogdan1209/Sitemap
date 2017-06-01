using Sitemap.Presentation.Models;
using Sitemap.Presentation.Models.SitemapData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sitemap.Presentation.Repositories
{
    public class SqlInjectionRepo
    {
        private ApplicationContext db;

        public SqlInjectionRepo(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(SqlInjectionModel item)
        {
            db.SqlInjection.Add(item);
        }


    }
}