using System.Linq.Expressions;

namespace Application.Common.Interfaces.Repositories
{
    public interface IGenericRepositoryService<TEntity> where TEntity : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false);
        Task<bool> AnyAsync(bool ignoreQueryFilters = false);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false);
        Task<int> CountAsync(bool ignoreQueryFilters = false);
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        Task<TEntity?> GetByIdAsync(long id, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        void HardDelete(TEntity entity);
        void HardDeleteRange(IEnumerable<TEntity> entities);
        Task<PaginatedList<TEntity>> PaginateAsync(int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        Task<PaginatedList<TEntity>> PaginateAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, IEnumerable<string>? includes = null, bool ignoreQueryFilters = false);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
