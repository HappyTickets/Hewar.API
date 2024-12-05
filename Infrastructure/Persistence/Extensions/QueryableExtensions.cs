using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    internal static class QueryableExtensions
    {
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> query, IEnumerable<string> includes) where TEntity : BaseEntity
            => includes.Aggregate(query, (acc, include) => acc.Include(include));

        public async static Task<PaginatedList<TEntity>> PaginateAsync<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class
        {
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PaginatedList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}
