using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class AdOfferConfigs : IEntityTypeConfiguration<AdOffer>
    {
        public void Configure(EntityTypeBuilder<AdOffer> builder)
        {
            builder
                .HasOne(a => a.Ad)
                .WithMany(a => a.AdOffers)
                .HasForeignKey(a => a.AdId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
