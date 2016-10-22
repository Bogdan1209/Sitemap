using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Sitemap.Presentation.Models.SitemapData;

namespace Sitemap.Presentation.Models
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<RequestHistory> RequestHistories { get; set; }
        public DbSet<SavedUrl> SavedUrls { get; set; }
        public DbSet<ResponseTime> ResponseTime { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

    }
}