﻿using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.DAL.Repositories.Realizations
{
    internal class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class //abstract??
    {
        private readonly IDbContextFactory<PawsitivelyCareDbContext> _contextFactory;

        public BaseRepository(IDbContextFactory<PawsitivelyCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            GetDbSet(context).Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await GetDbSet(context).FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using var context = _contextFactory.CreateDbContext();
            GetDbSet(context).Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using var context = _contextFactory.CreateDbContext();

            var entitiesList = entities.ToList();
            GetDbSet(context).AddRange(entitiesList);

            await context.SaveChangesAsync();

            return entitiesList;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            using var context = _contextFactory.CreateDbContext();
            GetDbSet(context).RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity?> QueryFirst(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var entities = await Query(filter, orderBy, 1, 0, includes);
            return entities.FirstOrDefault();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int count = 0, int offset = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<TEntity> query = GetDbSet(context).AsNoTracking();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (offset > 0)
                query = query.Skip(offset);

            if (count > 0)
                query = query.Take(count);

            if (orderBy is { })
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<int> QueryCount(Expression<Func<TEntity, bool>> filter)
        {
            using var context = _contextFactory.CreateDbContext();
            return await GetDbSet(context).AsNoTracking().Where(filter).CountAsync();
        }

        private DbSet<TEntity> GetDbSet(DbContext dbContext) => dbContext.Set<TEntity>();
    }
}
