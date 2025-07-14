using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(T obj);
        Task<bool> CheckIfExistAsync(Guid id);
    }
}
