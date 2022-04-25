using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class CountryDataConfiguration : IEntityTypeConfiguration<CountryData>
{
    public void Configure(EntityTypeBuilder<CountryData> builder)
    {
        builder.HasKey(
            e => e.CountryCode);

        builder
            .Property(e => e.ConcurrencyToken)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();

        builder
            .Property(e => e.CountryCode)
            .HasMaxLength(2);
        builder
            .Property(e => e.Currency)
            .HasMaxLength(3);
    }
}
