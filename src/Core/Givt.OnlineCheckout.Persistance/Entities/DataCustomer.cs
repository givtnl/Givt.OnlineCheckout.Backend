using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DataCustomer : DataEntityBase<Guid>
    {
        public string Email { get; set; }
        public string StripeCustomerReference { get; set; }
    }
}
