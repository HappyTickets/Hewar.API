using Domain.Entities.PriceRequestAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class PriceRequestConfigs : IEntityTypeConfiguration<PriceRequest>
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

            builder.HasOne(pr => pr.Response)
                .WithOne(r => r.PriceRequest)
                .HasForeignKey<PriceRequestOffer>(r => r.PriceRequestId);
            
            builder.HasOne(pr => pr.FacilityDetails)
                .WithOne(fd => fd.PriceRequest)
                .HasForeignKey<PriceRequestFacilityDetails>(fd => fd.PriceRequestId);

            builder.HasMany(pr => pr.Tickets)
                .WithOne(t => t.PriceRequest)
                .HasForeignKey(t => t.PriceRequestId);
        }
    }
}
