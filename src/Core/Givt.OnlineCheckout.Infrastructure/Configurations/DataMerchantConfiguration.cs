using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DataMerchantConfiguration : IEntityTypeConfiguration<Merchantdata>
    {
        public void Configure(EntityTypeBuilder<Merchantdata> builder)
        {
            
        }
    }
}
