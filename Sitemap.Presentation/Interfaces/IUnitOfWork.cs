using System;
using Sitemap.Presentation.Models.SitemapData;
using System.Threading.Tasks;
using Sitemap.Presentation.Repositories;

namespace Sitemap.Presentation.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHistoryRepo<RequestHistory> Histories { get; }
        ISavedUrlRepo<SavedUrl> SavedUrls { get; }
        IResponseTimeRepo<ResponseTime> ResponseTime { get; }
        IExtremeValues<ExtremeValues> ExtremeValues { get; }
        SqlInjectionRepo SqlInjection { get; }
        DosAttackRepo DosAttack { get; }
        Task SaveAsync();
    }
}
