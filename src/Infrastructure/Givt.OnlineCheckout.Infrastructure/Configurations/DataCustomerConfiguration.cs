using Givt.OnlineCheckout.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Givt.OnlineCheckout.Infrastructure.Configurations
{
    public class DataCustomerConfiguration : IEntityTypeConfiguration<DataCustomer>
    {
        public void Configure(EntityTypeBuilder<DataCustomer> builder)
        {

        }
    }
}
