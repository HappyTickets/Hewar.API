using AutoMapper;
using Domain.Entities.FacilityAggregate;
using Infrastructure.Persistence.Repositories.Generic;

namespace Infrastructure.Persistence.Repositories
{
    internal class FacilityRepositoryService(AppDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper) : SoftDeletableGenericRepositoryService<Facility>(dbContext, currentUserService, mapper)
    {
        public override void SoftDelete(Facility entity)
        {
            base.SoftDelete(entity);
        }
    }
}
