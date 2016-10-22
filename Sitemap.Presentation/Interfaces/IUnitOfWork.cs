using System;
using Sitemap.Presentation.Models.SitemapData;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHistoryRepo<RequestHistory> Histories { get; }
        ISavedUrlRepo<SavedUrl> SavedUrls { get; }
        IResponseTimeRepo<ResponseTime> ResponseTime { get; }
        Task SaveAsync();
    }
}
