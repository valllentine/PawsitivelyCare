using System.Linq.Expressions;

namespace PawsitivelyCare.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity?> QueryFirst(Expression<Func<TEntity, bool>>? filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
             params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int count = 0, int offset = 0,
            params Expression<Func<TEntity, object>>[] includes);
        Task<int> QueryCount(Expression<Func<TEntity, bool>> filter);
    }
}
