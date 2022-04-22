using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities;

public class CountryData : DataEntityBase<int>
{
    public string CountryCode { get; set; }
    public PaymentMethod PaymentMethods { get; set; }
    public string Currency { get; set; }
    public decimal ApplicationFeePercentage { get; set; }
    public decimal ApplicationFeeFixedAmount { get; set; }
}
