using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Interfaces
{
    public interface IExtremeValues<T> 
        where T: class
    {
        void Create(T item);
        void Update(T item);
    }
}
