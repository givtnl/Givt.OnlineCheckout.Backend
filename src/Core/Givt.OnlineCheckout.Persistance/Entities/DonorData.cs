using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class DonorData : DataEntityBase<Guid>
    {
        public string Email { get; set; } // email is stored normalised to lowercase
        public List<DonationData> Donations { get; set; }
    }
}
