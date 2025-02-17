using Application.Common.Utilities.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories.Generic
{
    internal class GenericRepositoryService<TEntity>(AppDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper) : IGenericRepositoryService<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _dbContext = dbContext;
        protected readonly ICurrentUserService _currentUserService = currentUserService;

        protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        #region Command

        public virtual async Task CreateAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public virtual void Create(TEntity entity)
        => _dbSet.Add(entity);

        public virtual void CreateRange(IEnumerable<TEntity> entities)
            => _dbSet.AddRange(entities);

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);
        public virtual void Update(TEntity entity)
            => _dbSet.Update(entity);

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
            => _dbSet.UpdateRange(entities);

        public virtual void HardDelete(TEntity entity)
            => _dbSet.Remove(entity);

        public virtual void HardDeleteRange(IEnumerable<TEntity> entities)
            => _dbSet.RemoveRange(entities);
        #endregion

        #region Query
        public virtual Task<TEntity?> GetByIdAsync(long id, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.Where(predicate).AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.ToListAsync();
        }

        public virtual async Task<PaginatedList<TEntity>> PaginateAsync(int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.PaginateAsync(pageNumber, pageSize);
        }

        public virtual Task<PaginatedList<TEntity>> PaginateAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.Where(predicate).AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return query.PaginateAsync(pageNumber, pageSize);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.AnyAsync(predicate);
        }

        public virtual Task<bool> AnyAsync(bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.AnyAsync();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.CountAsync(predicate);
        }

        public virtual Task<int> CountAsync(bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.CountAsync();
        }


        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await query.Where(predicate)
                               .ProjectTo<TResult>(mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TResult>> FilterAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await query.Where(predicate)
                               .ProjectTo<TResult>(mapper.ConfigurationProvider)
                               .ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return await query.ProjectTo<TResult>(mapper.ConfigurationProvider).ToListAsync();
        }





        #endregion
    }
}
