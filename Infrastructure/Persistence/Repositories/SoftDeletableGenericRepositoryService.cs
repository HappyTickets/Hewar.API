namespace Infrastructure.Persistence.Repositories
{
    internal class SoftDeletableGenericRepositoryService<TEntity> : GenericRepositoryService<TEntity>, ISoftDeletableGenericRepositoryService<TEntity> where TEntity : SoftDeletableEntity
    {
        public SoftDeletableGenericRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
        {
        }

        public void SoftDelete(SoftDeletableEntity entity)
        {
            entity.IsDeleted = true;

            entity.DeletedBy = _currentUserService.Id;
            entity.DeletedOn = DateTimeOffset.UtcNow;

            _dbContext.Update(entity);
        }

        public void SoftDeleteRange(IEnumerable<SoftDeletableEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;

                entity.DeletedBy = _currentUserService.Id;
                entity.DeletedOn = DateTimeOffset.UtcNow;
            }

            _dbContext.Update(entities);
        }

        public void Recover(SoftDeletableEntity entity)
        {
            entity.IsDeleted = false;
            _dbContext.Update(entity);
        }

        public void RecoverRange(IEnumerable<SoftDeletableEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }

            _dbContext.UpdateRange(entities);
        }
    }
}
