namespace Givt.OnlineCheckout.Persistance.Entities;

public class OrganisationTexts : LocalisableTexts
{
    public long OrganisationId { get; set; }
    public OrganisationData Organisation { get; set; }
}