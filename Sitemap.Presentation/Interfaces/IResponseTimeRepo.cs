using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Interfaces
{
    public interface IResponseTimeRepo<T>
        where T: class
    {
        void Create(T item);
        Task<List<T>> GetAsync(int historyId);
        double GetAverage(int historyId);
        void DeleteAsync(int historyId);
    }
}
