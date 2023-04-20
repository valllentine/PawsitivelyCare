using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        Task<int> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
