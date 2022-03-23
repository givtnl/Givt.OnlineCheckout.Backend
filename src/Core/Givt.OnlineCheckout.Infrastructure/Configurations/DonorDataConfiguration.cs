using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DonorDataConfiguration : IEntityTypeConfiguration<DonorData>
    {
        public void Configure(EntityTypeBuilder<DonorData> builder)
        {
            builder
                .Property(e => e.Email)
                .HasMaxLength(70); // 254 according to RFC 2821, we limit to 70

            builder.HasIndex(e => e.Email);
        }
    }
}
