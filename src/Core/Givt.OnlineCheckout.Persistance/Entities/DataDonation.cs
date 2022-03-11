using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DataDonation : DataEntityBase<int>
    {        
        public decimal Amount { get; set; }
        public string? PaymentProviderTransactionReference { get; set; }
    }
}
