using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class CountryDataConfiguration : IEntityTypeConfiguration<CountryData>
    {
        public void Configure(EntityTypeBuilder<CountryData> builder)
        {
            builder
                .Property(e => e.CountryCode)
                .HasMaxLength(2);
            builder
                .Property(e => e.Currency)
                .HasMaxLength(3);
            builder
                .HasIndex(e => e.CountryCode);
        }
    }
}
