namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class Goal
{
    public string Title { get; set; }
    public string Name { get; set; } // Goal
    public IList<Donation> Donations { get; set; }
    public IList<CurrencyAmount> TotalAmount { get; set; }
}
