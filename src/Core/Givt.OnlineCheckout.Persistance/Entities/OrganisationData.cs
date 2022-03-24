using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class OrganisationData : DataEntityBase<long>
    {
        public IEnumerable<MediumData> Mediums { get; set; }
        public string Name { get; set; }
        public string PaymentProviderAccountReference { get; set; }
        public string Namespace { get; set; }
        public string Currency { get; set; }
        public bool Active { get; set; }
    }
}
