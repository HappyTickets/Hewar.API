using Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Repositories
{
    internal class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWorkService(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public ISoftDeletableGenericRepositoryService<TEntity> GetRepository<TEntity>() where TEntity : SoftDeletableEntity
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (ISoftDeletableGenericRepositoryService<TEntity>)repository;
            }

            var newRepository = new SoftDeletableGenericRepositoryService<TEntity>(_context, _currentUserService);
            _repositories[entityType] = newRepository;

            return newRepository;
        }

        #region Transaction Methods
        public Task<IDbContextTransaction> BeginTransactionAsync()
            => _context.Database.BeginTransactionAsync();

        public Task CommitTransactionAsync()
            => _context.Database.CommitTransactionAsync();

        public Task RollbackTransactionAsync()
            => _context.Database.RollbackTransactionAsync();

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
        #endregion
    }

}
