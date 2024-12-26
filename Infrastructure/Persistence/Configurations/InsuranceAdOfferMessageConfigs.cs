using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class InsuranceAdOfferMessageConfigs : IEntityTypeConfiguration<InsuranceAdOfferMessage>
    {
        public void Configure(EntityTypeBuilder<InsuranceAdOfferMessage> builder)
        {
            builder.OwnsMany(m => m.Medias);
        }
    }
}
