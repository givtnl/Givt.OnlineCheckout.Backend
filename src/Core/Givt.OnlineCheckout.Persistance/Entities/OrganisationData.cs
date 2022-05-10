using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class OrganisationData : DataEntityBase<long>
    {
        public string CountryCode { get; set; }
        public CountryData Country { get; set; }
        public ICollection<MediumData> Mediums { get; set; }
        public string Name { get; set; }
        public string PaymentProviderAccountReference { get; set; }
        public string Namespace { get; set; }
        public string LogoImageLink { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public bool Active { get; set; }
        public bool TaxDeductable { get; set; }

        // NL
        public string RSIN { get; set; }

        // UK
        public string HmrcReference { get; set; }
        public string CharityNumber { get; set; }
    }
}
