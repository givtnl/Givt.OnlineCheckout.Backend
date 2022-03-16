using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities;

public class DataMedium: DataEntityBase<long>
{
    public DataMerchant Merchant { get; set; }
    public long MerchantId { get; set; }
    public string Amounts { get; set; }
    public string ThankYou { get; set; }
    public string Goal { get; set; }
    public string Medium { get; set; }
}