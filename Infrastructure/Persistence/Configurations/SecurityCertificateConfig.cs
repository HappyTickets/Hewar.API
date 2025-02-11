using Domain.Entities.FacilityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SecurityCertificateConfig : IEntityTypeConfiguration<SecurityCertificate>
    {
        public void Configure(EntityTypeBuilder<SecurityCertificate> builder)
        {
            builder.HasOne(sc => sc.Address)
                .WithOne()
                .HasForeignKey<SecurityCertificate>(sc => sc.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.Facility)
                .WithMany(f => f.SecurityCertificates)
                .HasForeignKey(sc => sc.FacilityId)
                .OnDelete(DeleteBehavior.NoAction); // Ensure no cascading delete
        }
    }

}
