using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class GuardConfigs : IEntityTypeConfiguration<Guard>
    {
        public void Configure(EntityTypeBuilder<Guard> builder)
        {
            builder.OwnsMany(g => g.Skills);
            builder.OwnsMany(g => g.PrevCompanies);
        }
    }
}
