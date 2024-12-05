
namespace Application.Common.Interfaces.Repositories
{
    public interface ISoftDeletableGenericRepositoryService<TEntity> : IGenericRepositoryService<TEntity> where TEntity : SoftDeletableEntity
    {
        void Recover(SoftDeletableEntity entity);
        void RecoverRange(IEnumerable<SoftDeletableEntity> entities);
        void SoftDelete(SoftDeletableEntity entity);
        void SoftDeleteRange(IEnumerable<SoftDeletableEntity> entities);
    }
}
