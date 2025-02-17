//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Persistence.Configurations
//{

//    public class ContractTemplateConfigs : IEntityTypeConfiguration<ContractTemplate>
//    {
//        public void Configure(EntityTypeBuilder<ContractTemplate> builder)
//        {

//            builder.HasKey(oc => oc.Id);

//            builder.HasOne(oc => oc.Offer)
//              .WithOne()
//              .HasForeignKey<ContractTemplate>(oc => oc.OfferId)
//              .OnDelete(DeleteBehavior.Cascade);

//        }
//    }

//}
