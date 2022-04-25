using Givt.OnlineCheckout.Persistance.Models;

namespace Givt.OnlineCheckout.Persistance.Entities;

public class MediumData: DataEntityBase<long>
{
    public long OrganisationId { get; set; }
    public OrganisationData Organisation { get; set; }
    public string Amounts { get; set; }
    public ICollection<MediumTexts> Texts { get; set; }
    public string Medium { get; set; }

}