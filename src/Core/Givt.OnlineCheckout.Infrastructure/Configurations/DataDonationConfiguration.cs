﻿using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DataDonationConfiguration : IEntityTypeConfiguration<DonationData>
    {
        public void Configure(EntityTypeBuilder<DonationData> builder)
        {
            
        }
    }
}
