
namespace Application.Common.Interfaces.Repositories
{
    public interface ISoftDeletableGenericRepositoryService<TEntity> : IGenericRepositoryService<TEntity> where TEntity : SoftDeletableEntity
    {
        void Recover(TEntity entity);
        void RecoverRange(IEnumerable<TEntity> entities);
        void SoftDelete(TEntity entity);
        void SoftDeleteRange(IEnumerable<TEntity> entities);
    }
}
