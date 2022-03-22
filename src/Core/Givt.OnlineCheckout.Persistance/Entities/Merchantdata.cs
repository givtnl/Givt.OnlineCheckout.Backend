using System;
using Givt.OnlineCheckout.Persistance.Enums;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class Merchantdata : DataEntityBase<long>
    {
        public IEnumerable<MediumData> Mediums { get; set; }
        public string? Name { get; set; }
        public string? PaymentProviderAccountReference { get; set; }
        public string? Namespace { get; set; }
        public Currency Currency { get; set; }
        public bool Active { get; set; }
    }
}
