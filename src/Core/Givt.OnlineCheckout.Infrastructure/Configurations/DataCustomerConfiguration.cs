using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DataCustomerConfiguration : IEntityTypeConfiguration<CustomerData>
    {
        public void Configure(EntityTypeBuilder<CustomerData> builder)
        {
            builder.Property(e => e.Email).HasMaxLength(254); // according to RFC 2821

            builder.HasIndex(e => e.Email);
        }
    }
}
