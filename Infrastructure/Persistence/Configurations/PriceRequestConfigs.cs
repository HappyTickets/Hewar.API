using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PriceRequestConfigs : IEntityTypeConfiguration<PriceRequest>
    {
        public void Configure(EntityTypeBuilder<PriceRequest> builder)
        {
            builder.HasOne(pr => pr.Company)
                .WithMany(c => c.PriceRequests)
                .HasForeignKey(pr => pr.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pr => pr.Facility)
                .WithMany(f => f.PriceRequests)
                .HasForeignKey(pr => pr.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pr => pr.Offers)
                .WithOne(po => po.PriceRequest)
                .HasForeignKey(po => po.PriceRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
