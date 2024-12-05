using Infrastructure.Persistence.Extensions;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    internal class GenericRepositoryService<TEntity>: IGenericRepositoryService<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        protected readonly ICurrentUserService _currentUserService;

        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _currentUserService = currentUserService;
        }

        #region Command

        public void Create(TEntity entity)
            => _dbSet.Add(entity);

        public void CreateRange(IEnumerable<TEntity> entities)
            => _dbSet.AddRange(entities);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void UpdateRange(IEnumerable<TEntity> entities)
            => _dbSet.UpdateRange(entities);

        public void HardDelete(TEntity entity)
            => _dbSet.Remove(entity);

        public void HardDeleteRange(IEnumerable<TEntity> entities)
            => _dbSet.RemoveRange(entities);
        #endregion

        #region Query
        public Task<TEntity?> GetByIdAsync(long id, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.Where(predicate).AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.ToListAsync();
        }

        public async Task<PaginatedList<TEntity>> PaginateAsync(int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return await query.PaginateAsync(pageNumber, pageSize);
        }

        public Task<PaginatedList<TEntity>> PaginateAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.Where(predicate).AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (includes != null)
                query = query.Include(includes);

            return query.PaginateAsync(pageNumber, pageSize);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.AnyAsync(predicate);
        }

        public Task<bool> AnyAsync(bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.AnyAsync();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.CountAsync(predicate);
        }

        public Task<int> CountAsync(bool ignoreQueryFilters = false)
        {
            var query = _dbSet.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query.CountAsync();
        }
        #endregion
    }
}
