using Givt.OnlineCheckout.Infrastructure.Converters;
using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class MediumDataConfiguration : IEntityTypeConfiguration<MediumData>
{
    public void Configure(EntityTypeBuilder<MediumData> builder)
    {
        builder
            .Property(e => e.ConcurrencyToken)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();

        builder
            .HasOne(x => x.Organisation)
            .WithMany(x => x.Mediums)
            .HasForeignKey(x => x.OrganisationId);

        builder
           .Property(e => e.Medium)
           .HasMaxLength(33);

        builder
            .Property(e => e.Amounts)
            .HasConversion(AmountsConverter.GetConverter())
            .HasMaxLength(50)
            .Metadata.SetValueComparer(AmountsConverter.GetComparer());


        builder.HasMany(e => e.Texts)
            .WithOne(t => t.Medium)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
