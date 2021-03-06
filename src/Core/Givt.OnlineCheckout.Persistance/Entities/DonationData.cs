using System.Globalization;
using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DonationData : DataEntityBase<long>
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string TransactionReference { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int TimezoneOffset { get; set; }        
        public DonorData Donor { get; set; }
        public MediumData Medium { get; set; }
        public DonationStatus Status { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string Last4 { get; set; }
        public string Fingerprint { get; set; }

        //public DateTime? CancelledAt { get; set; }
        //public string CancellationReason { get; set; }      

    }
}
