using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ApplicationRolesConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasIndex(x => x.NormalizedName).IsUnique(false);
            //builder.HasIndex(x => new { x.NormalizedName, x.TenantId }).IsUnique();
        }
    }
}
