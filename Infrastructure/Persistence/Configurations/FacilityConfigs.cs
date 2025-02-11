using Domain.Entities.FacilityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    class FacilityConfigs : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasOne(c => c.Address)
               .WithOne()
               .HasForeignKey<Facility>(c => c.AddressId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.SecurityCertificates)
             .WithOne(sc => sc.Facility)
             .HasForeignKey(sc => sc.FacilityId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}