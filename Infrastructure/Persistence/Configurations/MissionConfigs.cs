using Domain.Entities.MissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MissionConfigs : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.HasOne(m => m.Facility)
                   .WithMany()
                   .HasForeignKey(m => m.FacilityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Company)
                   .WithMany()
                   .HasForeignKey(m => m.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
