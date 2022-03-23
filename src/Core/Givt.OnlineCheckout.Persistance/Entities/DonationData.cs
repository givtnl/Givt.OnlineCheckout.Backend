using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DonationData : DataEntityBase<int>
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentProviderTransactionReference { get; set; }

        public CustomerData? Customer { get; set; }        
        public DonationStatus Status { get; set; }
        
        //public DateTime? CancelledAt { get; set; }
        //public string? CancellationReason { get; set; }      

    }
}
