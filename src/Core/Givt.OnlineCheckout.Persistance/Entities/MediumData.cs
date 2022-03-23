using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities;

public class MediumData: DataEntityBase<long>
{
    public OrganisationData Organisation { get; set; }
    public long OrganisationId { get; set; }
    public string Amounts { get; set; }
    public string ThankYou { get; set; }
    public string Goal { get; set; }
    public string Medium { get; set; }
}