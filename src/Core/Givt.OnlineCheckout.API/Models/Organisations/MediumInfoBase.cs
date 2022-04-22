namespace Givt.OnlineCheckout.API.Models.Mediums;

public class MediumInfoBase
{
    public long OrganisationId { get; set; }
    public string Medium { get; set; }
    public decimal[] Amounts { get; set; }
}