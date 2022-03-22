using System;
using System.Collections.Generic;
using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class CustomerData : DataEntityBase<Guid>
    {
        public string? Email { get; set; } // TODO: make sure email is normalised
        public List<DonationData>? Donations { get; set; }
    }
}
