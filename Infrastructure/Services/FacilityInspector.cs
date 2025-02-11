using Domain.Entities.FacilityAggregate;

namespace Infrastructure.Services
{
    public class FacilityInspector(IUnitOfWorkService ufw) : IFacilityInspector
    {

        public async Task<bool> IsAuthorized(long? facilityId)
        {
            if (!facilityId.HasValue)
            {
                return false;
            }
            var hasActiveContract = await ufw
                .GetRepository<SecurityCertificate>()
                .AnyAsync(sc => sc.Status == ContractStatus.Verified && sc.EndDate > DateTimeOffset.UtcNow && sc.FacilityId == facilityId);

            if (hasActiveContract)
            {
                return true;
            }


            var transacted = await ufw
                .GetRepository<PriceRequest>()
                .AnyAsync(pr => pr.FacilityId == facilityId && pr.Offers.Any());

            return !transacted;
        }
    }
}
