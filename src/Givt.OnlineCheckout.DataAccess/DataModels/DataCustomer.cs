using Givt.OnlineCheckout.DataAccess.Infrastructure;

namespace Givt.OnlineCheckout.DataAccess.DataModels
{
    public class DataCustomer : DataEntityBase<Guid>
    {
        public string Email { get; set; }
        public string StripeCustomerReference { get; set; }
    }
}
