using System.Collections.Generic;
using System.Linq;
using Sitemap.Presentation.Models.SitemapData;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Interfaces
{
    public interface ISavedUrlRepo<T>
        where T: class
    {
        Task<List<T>> GetAsync(int id);
        List<T> FindByDomain(string url);
        Task<T> FindByUrlAsync(string url);
        void Create(T item);
        void CreateList(List<T> urls);
        void Update(T item);
        Task<List<UrlViewModel>> GetRespondTimeAsync(int historyId);
    }
}
