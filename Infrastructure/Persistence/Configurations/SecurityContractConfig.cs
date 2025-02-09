using Domain.Entities.FacilityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SecurityContractConfig : IEntityTypeConfiguration<SecurityContract>
    {
        public void Configure(EntityTypeBuilder<SecurityContract> builder)
        {
            builder.HasOne(sc => sc.Address)
                .WithOne()
                .HasForeignKey<SecurityContract>(sc => sc.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.Facility)
                .WithMany(f => f.SecurityContracts)
                .HasForeignKey(sc => sc.FacilityId)
                .OnDelete(DeleteBehavior.NoAction); // Ensure no cascading delete
        }
    }

}
