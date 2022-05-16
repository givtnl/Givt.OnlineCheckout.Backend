using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities;

public class CountryData : AuditableEntity
{
    public string CountryCode { get; set; }
    public uint ConcurrencyToken { get; set; }
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    public string Currency { get; set; }
    public string Locale { get; set; }
    public string GivtName { get; set; }
    public string GivtAddress { get; set; }
    public string GivtEmail { get; set; }
    public string GivtPhoneNumber { get; set; }
    public string GivtWebsite { get; set; }
    public string GivtWantKnowMore { get; set; }
    public string GivtPrivacyPolicy { get; set; }
    public decimal ApplicationFeePercentage { get; set; }
    public decimal ApplicationFeeFixedAmount { get; set; }
}
