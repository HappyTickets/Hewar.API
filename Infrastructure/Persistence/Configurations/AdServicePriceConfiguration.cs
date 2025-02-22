using Domain.Entities.AdAggregate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AdServicePriceConfiguration : IEntityTypeConfiguration<AdHewarServiceCost>
    {
        public void Configure(EntityTypeBuilder<AdHewarServiceCost> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
