namespace Givt.OnlineCheckout.API.Models;

public class MediumDetailModel
{
    public decimal[] Amounts { get; set; }
    public string ThankYou { get; set; }
    public string Goal { get; set; }
    public string Medium { get; set; }
    public string OrganisationName { get; set; }
    public Currency Currency { get; set; }
}

public enum Currency
{
    EUR,
    GBP,
    USD
}