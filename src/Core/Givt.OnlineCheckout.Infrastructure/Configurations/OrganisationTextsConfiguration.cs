using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class OrganisationTextsConfiguration : IEntityTypeConfiguration<OrganisationTexts>
{
    public void Configure(EntityTypeBuilder<OrganisationTexts> builder)
    {

        //builder
        //    .Property(e => e.Goal)
        //    .HasMaxLength ... "unlimited"

        //builder
        //    .Property(e => e.ThankYou)
        //    .HasMaxLength ... "unlimited"

    }
}
