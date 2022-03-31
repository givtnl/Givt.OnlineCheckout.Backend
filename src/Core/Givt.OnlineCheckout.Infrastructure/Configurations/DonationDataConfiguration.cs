using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DonationDataConfiguration : IEntityTypeConfiguration<DonationData>
    {
        public void Configure(EntityTypeBuilder<DonationData> builder)
        {
            builder
                .Property(e => e.Currency)
                .HasMaxLength(3);

            builder
                .Property(e => e.TransactionReference)
                .HasMaxLength(50); // Stripe seems to use 27 characters

            builder
                .Property(e => e.LanguageId)
                .HasMaxLength(20);

            builder
                .HasOne(e => e.Donor)
                .WithMany(d => d.Donations)
                .IsRequired(false);

            builder
                .HasIndex(e => e.TransactionReference);
        }
    }
}