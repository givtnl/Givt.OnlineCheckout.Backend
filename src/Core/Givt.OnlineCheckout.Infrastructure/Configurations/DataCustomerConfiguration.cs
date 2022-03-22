using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DataCustomerConfiguration : IEntityTypeConfiguration<CustomerData>
    {
        public void Configure(EntityTypeBuilder<CustomerData> builder)
        {

        }
    }
}
