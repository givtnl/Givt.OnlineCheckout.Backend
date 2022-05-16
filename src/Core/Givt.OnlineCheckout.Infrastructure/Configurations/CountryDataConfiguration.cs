using Givt.OnlineCheckout.Infrastructure.Converters;
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

        builder.Property(x => x.PaymentMethods)
            .HasColumnType(nameof(UInt64))
            .HasConversion(PaymentMethodsConverter.GetConverter())
            .Metadata.SetValueComparer(PaymentMethodsConverter.GetComparer());

        builder
            .Property(e => e.Currency)
            .HasMaxLength(3);
        builder
            .Property(e => e.Locale)
            .HasMaxLength(20); // length at least 14 (ca-ES-valencia = catalan spain valencia dialect)

        builder
            .Property(e => e.GivtName)
            .HasMaxLength(100);
        builder
            .Property(e => e.GivtAddress)
            .HasMaxLength(200);
        builder
            .Property(e => e.GivtEmail)
            .HasMaxLength(200);
        builder
            .Property(e => e.GivtPhoneNumber)
            .HasMaxLength(200);
        builder
            .Property(e => e.GivtWebsite)
            .HasMaxLength(200);
        builder
            .Property(e => e.GivtWantKnowMore)
            .HasMaxLength(200);
        builder
            .Property(e => e.GivtPrivacyPolicy)
            .HasMaxLength(200);
    }
}
