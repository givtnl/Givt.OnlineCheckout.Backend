using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations;

public class OrganisationTextsConfiguration : IEntityTypeConfiguration<OrganisationTexts>
{
    public void Configure(EntityTypeBuilder<OrganisationTexts> builder)
    {
        builder
            .Property(e => e.ConcurrencyToken)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();

        builder
            .Property(e => e.Goal)
            .HasMaxLength(400);
        builder
            .Property(e => e.ThankYou)
            .HasMaxLength(400);

        builder
            .HasKey(x => new { x.OrganisationId, x.LanguageId });
    }
}
