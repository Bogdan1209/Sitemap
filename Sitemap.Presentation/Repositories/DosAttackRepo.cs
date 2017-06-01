using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitemap.Presentation.Models;
using Sitemap.Presentation.Models.SitemapData;

namespace Sitemap.Presentation.Repositories
{
    public class DosAttackRepo
    {
        private ApplicationContext db;

        public DosAttackRepo(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(DosAttackModel item)
        {
            db.DosAttacks.Add(item);
        }
    }
}