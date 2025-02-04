using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PriceOfferConfigs : IEntityTypeConfiguration<PriceOffer>
    {
        public void Configure(EntityTypeBuilder<PriceOffer> builder)
        {
            builder.HasKey(p => p.Id);

            // Relationship with PriceRequest
            builder.HasOne(p => p.PriceRequest)
                   .WithMany(pr => pr.Offers)
                   .HasForeignKey(p => p.PriceRequestId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            // Services Relationship
            builder.HasMany(p => p.Services)
                   .WithOne(p => p.PriceOffer)
                   .HasForeignKey(p => p.PriceOfferId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional Chat relationship
            builder.HasOne(p => p.Chat)
                   .WithOne()
                   .HasForeignKey<PriceOffer>(p => p.ChatId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
