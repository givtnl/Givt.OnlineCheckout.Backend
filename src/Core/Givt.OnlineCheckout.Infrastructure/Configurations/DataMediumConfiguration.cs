using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class DataMediumConfiguration:  IEntityTypeConfiguration<DataMedium>
{
    public void Configure(EntityTypeBuilder<DataMedium> builder)
    {
        builder.HasOne(x => x.Merchant).WithMany(x => x.Mediums).HasForeignKey(x => x.MerchantId);
    }
}