using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class MediumDataConfiguration : IEntityTypeConfiguration<MediumData>
{
    public void Configure(EntityTypeBuilder<MediumData> builder)
    {
        builder
            .HasOne(x => x.Organisation)
            .WithMany(x => x.Mediums)
            .HasForeignKey(x => x.OrganisationId);

        builder
           .Property(e => e.Medium)
           .HasMaxLength(33);

        builder
            .Property(e => e.Amounts)
            .HasMaxLength(50);

        //builder
        //    .Property(e => e.Goal)
        //    .HasMaxLength ... "unlimited"

        //builder
        //    .Property(e => e.ThankYou)
        //    .HasMaxLength ... "unlimited"

    }
}
