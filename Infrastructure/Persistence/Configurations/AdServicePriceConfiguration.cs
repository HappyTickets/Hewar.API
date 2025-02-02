using Domain.Entities.AdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AdServicePriceConfiguration : IEntityTypeConfiguration<AdServicePrice>
    {
        public void Configure(EntityTypeBuilder<AdServicePrice> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
