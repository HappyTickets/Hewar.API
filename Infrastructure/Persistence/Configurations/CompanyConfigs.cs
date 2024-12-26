using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class CompanyConfigs : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(c => c.InsuranceAdOffers)
                .WithOne(o=>o.Company)
                .HasForeignKey(o=>o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
