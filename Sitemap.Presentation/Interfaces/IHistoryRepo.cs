using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Interfaces
{
    public interface IHistoryRepo<T>
        where T: class
    {
        void Create(T item);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindByUserIdAsync(string userId);
        Task<T> FindByDomainAsync(string domain);
        void Update(T item);
        Task DeleteAsync(int id);
    }
}
