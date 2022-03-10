using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DataMerchant : DataEntityBase<Guid>
    {
        public string Name { get; set; }
        public string PaymentProviderAccountReference { get; set; }
        public string Namespace { get; set; }
    }
}
