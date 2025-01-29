using Domain.Entities.CompanyAggregate;
using Infrastructure.Persistence.Repositories.Generic;

namespace Infrastructure.Persistence.Repositories
{
    internal class CompanyRepositoryService : SoftDeletableGenericRepositoryService<Company>
    {
        public CompanyRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
        {
        }

        public override void SoftDelete(Company entity)
        {
            base.SoftDelete(entity);
        }
    }
}
