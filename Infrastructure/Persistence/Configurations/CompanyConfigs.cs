using Domain.Entities.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class CompanyConfigs : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(c => c.InsuranceAdOffers)
                .WithOne(o => o.Company)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Address)
               .WithOne()
               .HasForeignKey<Company>(c => c.AddressId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Services)
           .WithOne(cs => cs.Company)
           .HasForeignKey(cs => cs.CompanyId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
