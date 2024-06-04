using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejki.ApplicationCore.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T?> Update(T entity);
        Task Delete(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(string id);
    }
}
