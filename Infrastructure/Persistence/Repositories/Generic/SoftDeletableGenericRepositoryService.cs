using AutoMapper;

namespace Infrastructure.Persistence.Repositories.Generic
{
    internal class SoftDeletableGenericRepositoryService<TEntity> : GenericRepositoryService<TEntity>, ISoftDeletableGenericRepositoryService<TEntity> where TEntity : SoftDeletableEntity
    {
        public SoftDeletableGenericRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper) : base(dbContext, currentUserService, mapper)
        {
        }

        public virtual void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;

            entity.DeletedBy = _currentUserService.UserId.ToString();
            entity.DeletedOn = DateTimeOffset.UtcNow;

            _dbContext.Update(entity);
        }

        public virtual void SoftDeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;

                entity.DeletedBy = _currentUserService.UserId.ToString();
                entity.DeletedOn = DateTimeOffset.UtcNow;
            }

            _dbContext.Update(entities);
        }

        public virtual void Recover(TEntity entity)
        {
            entity.IsDeleted = false;
            _dbContext.Update(entity);
        }

        public virtual void RecoverRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }

            _dbContext.UpdateRange(entities);
        }
    }
}
