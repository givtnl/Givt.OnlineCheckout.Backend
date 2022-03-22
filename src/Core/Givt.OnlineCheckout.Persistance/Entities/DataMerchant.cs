using System;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DataMerchant : DataEntityBase<long>
    {
        public IEnumerable<DataMedium> Mediums { get; set; }
        public string? Name { get; set; }
        public string? PaymentProviderAccountReference { get; set; }
        public string? Namespace { get; set; }
        public Currency Currency { get; set; }
        public bool Active { get; set; }
    }

    public enum Currency
    {
        EUR,
        GBP,
        USD
    }
}
