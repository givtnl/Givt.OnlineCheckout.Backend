namespace Givt.OnlineCheckout.Business.Models.Report;

public class Goal
{
    public string Name { get; set; }
    public IList<Donation> Donations { get; set; }
    public IList<CurrencyAmount> TotalAmount { get; set; }
}
