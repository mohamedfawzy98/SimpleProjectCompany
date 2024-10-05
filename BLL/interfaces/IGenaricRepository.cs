using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IGenaricRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetIdAsync(int? id);
        Task<int> AddAsync(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
