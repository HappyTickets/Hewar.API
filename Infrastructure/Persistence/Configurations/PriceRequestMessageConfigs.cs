﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class PriceRequestMessageConfigs : IEntityTypeConfiguration<PriceRequestMessage>
    {
        public void Configure(EntityTypeBuilder<PriceRequestMessage> builder)
        {
            builder.OwnsMany(m => m.Medias);
        }
    }
}
