using Infrastructure.Persistence.Repositories.Generic;

namespace Infrastructure.Persistence.Repositories
{
    internal class FacilityRepositoryService : SoftDeletableGenericRepositoryService<Facility>
    {
        public FacilityRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService) : base(dbContext, currentUserService)
        {
        }

        public override void SoftDelete(Facility entity)
        {
            base.SoftDelete(entity);
            entity.LoginDetails.IsDeleted = true;
        }
    }
}
