namespace Givt.OnlineCheckout.API.Models.Mediums;

public class MediumResponse
{
    public long OrganisationId { get; set; }
    public string Medium { get; set; }
    public decimal[] Amounts { get; set; }
}