using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class OrganisationData : DataEntityBase<long>
    {
        public CountryData Country { get; set; }    
        public IEnumerable<MediumData> Mediums { get; set; }
        public string Name { get; set; }
        public ICollection<OrganisationTexts> Texts { get; set; }
        public string PaymentProviderAccountReference { get; set; }
        public string Namespace { get; set; }
        public string Currency { get; set; }
        public string LogoImageLink { get; set; }
        public PaymentMethod PaymentMethods { get; set; }
        public bool Active { get; set; }
    }
}
