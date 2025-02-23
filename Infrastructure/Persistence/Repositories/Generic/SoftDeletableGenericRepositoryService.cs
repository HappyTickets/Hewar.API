﻿namespace Infrastructure.Persistence.Repositories.Generic
{
    internal class SoftDeletableGenericRepositoryService<TEntity> : GenericRepositoryService<TEntity>, ISoftDeletableGenericRepositoryService<TEntity> where TEntity : SoftDeletableEntity
    {
        public SoftDeletableGenericRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
        {
        }

        public virtual void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;

            entity.DeletedBy = _currentUserService.IdentityId.ToString();
            entity.DeletedOn = DateTimeOffset.UtcNow;

            _dbContext.Update(entity);
        }

        public virtual void SoftDeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;

                entity.DeletedBy = _currentUserService.IdentityId.ToString();
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
