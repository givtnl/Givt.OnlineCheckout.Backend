using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Givt.OnlineCheckout.Persistance.Enums
{
    public enum DonationStatus
    {
        Created = 0,
        Processing = 1,
        Succeeded = 2,
        PaymentFailed = 3,
        Cancelled = 4,
        Revoked = 5, // e.g. direct debit reversal
    }
}
