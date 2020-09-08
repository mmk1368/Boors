using Boors.Models;
using InstaMarket.Web.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InstaMarket.Web.Core.Repository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private BourseContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        private IQueryable<TEntity> _query;

        public BaseRepository(BourseContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
            _query = _dbSet;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbContext.Set<TEntity>()
                .Include(_dbContext.GetIncludePaths(typeof(TEntity)));
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> whereCluse = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            string includes = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (whereCluse != null)
            {
                query = query.Where(whereCluse);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (string include in includes.Split(","))
                {
                    query = query.Include(include.Trim()).IgnoreQueryFilters();
                }
            }
            return query;
        }
        public virtual ValueTask<TEntity> GetByIdAsync(object id)
        {
            return _dbSet.FindAsync(id);
        }

        public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return _dbSet.AddAsync(entity);
        }

        public virtual EntityEntry<TEntity> UpdateAsync(TEntity entity)
        {
            return _dbSet.Update(entity);
        }

        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteByIdAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            return Delete(entity);
        }

        public virtual Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        public virtual Task<PaginatedList<TEntity>> PaginatedListAsync(int pageNumber, int perPage)
        {
            return PaginatedList<TEntity>.CreateAsync(_query, pageNumber, perPage);
        }
    }
}
