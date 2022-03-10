using Givt.OnlineCheckout.DataAccess.Infrastructure;

namespace Givt.OnlineCheckout.DataAccess.DataModels
{
    public class DataMerchant : DataEntityBase<Guid>
    {
        public string Name { get; set; }
        public string PaymentProviderAccountReference { get; set; }
        public string Namespace { get; set; }
    }
}
