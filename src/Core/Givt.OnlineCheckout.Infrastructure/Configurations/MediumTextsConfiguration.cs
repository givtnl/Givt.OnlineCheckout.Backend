using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class MediumTextsConfiguration : IEntityTypeConfiguration<MediumTexts>
{
    public void Configure(EntityTypeBuilder<MediumTexts> builder)
    {

        //builder
        //    .Property(e => e.Goal)
        //    .HasMaxLength ... "unlimited"

        //builder
        //    .Property(e => e.ThankYou)
        //    .HasMaxLength ... "unlimited"

    }
}
