using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PriceRequestOfferConfigs : IEntityTypeConfiguration<PriceOffer>
    {
        public void Configure(EntityTypeBuilder<PriceOffer> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Relationships
            builder.HasOne(p => p.PriceRequest)
                   .WithMany()
                   .HasForeignKey(p => p.PriceRequestId)
                   .OnDelete(DeleteBehavior.ClientSetNull); // Disable cascade delete

            builder.HasMany(p => p.Services)
                   .WithOne(p => p.PriceOffer)
                   .HasForeignKey(p => p.PriceOfferId)
                   .OnDelete(DeleteBehavior.Cascade); // Allow cascade delete for PriceOfferService

            // Optional Chat relationship
            builder.HasOne(p => p.Chat)
                   .WithOne()
                   .HasForeignKey<PriceOffer>(p => p.ChatId)
                   .OnDelete(DeleteBehavior.SetNull); // Optional, set to null on delete
        }
    }
}
