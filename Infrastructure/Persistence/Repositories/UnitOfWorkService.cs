using AutoMapper;
using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Repositories
{
    internal class UnitOfWorkService(AppDbContext context, ICurrentUserService currentUserService, IMapper mapper) : IUnitOfWorkService
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public ISoftDeletableGenericRepositoryService<TEntity> GetRepository<TEntity>() where TEntity : SoftDeletableEntity
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (ISoftDeletableGenericRepositoryService<TEntity>)repository;
            }

            var newRepository = new SoftDeletableGenericRepositoryService<TEntity>(context, currentUserService, mapper);
            _repositories[entityType] = newRepository;

            return newRepository;
        }

        #region Transaction Methods
        public Task<IDbContextTransaction> BeginTransactionAsync()
            => context.Database.BeginTransactionAsync();

        public Task CommitTransactionAsync()
            => context.Database.CommitTransactionAsync();

        public Task RollbackTransactionAsync()
            => context.Database.RollbackTransactionAsync();

        public Task SaveChangesAsync()
            => context.SaveChangesAsync();
        #endregion
    }

}
