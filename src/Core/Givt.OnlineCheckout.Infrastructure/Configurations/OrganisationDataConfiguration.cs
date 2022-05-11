using Givt.OnlineCheckout.Infrastructure.Converters;
using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class OrganisationDataConfiguration : IEntityTypeConfiguration<OrganisationData>
    {
        public void Configure(EntityTypeBuilder<OrganisationData> builder)
        {
            builder
                .Property(e => e.ConcurrencyToken)
                .HasColumnName("xmin")
                .HasColumnType("xid")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder
                .Property(e => e.Name)
                .HasMaxLength(175);

            builder
                .Property(e => e.PaymentProviderAccountReference)
                .HasMaxLength(50);

            builder
                .Property(e => e.Namespace)
                .HasMaxLength(20);

            builder.Property(x => x.LogoImageLink)
                .HasMaxLength(200);

            builder.Property(x => x.PaymentMethods)
                .HasColumnType(nameof(UInt64))
                .HasConversion(PaymentMethodsConverter.GetConverter())
                .Metadata.SetValueComparer(PaymentMethodsConverter.GetComparer());

            builder.Property(x => x.RSIN)
                .HasMaxLength(50); // only 9 or 14 needed? 9 + 1 + 2 + 2
            builder.Property(x => x.HmrcReference)
                .HasMaxLength(20);
            builder.Property(x => x.CharityNumber)
                .HasMaxLength(35);

            builder
                .HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryCode);


            builder
                .HasIndex(e => e.Namespace);
        }
    }
}
