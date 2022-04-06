namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public class Organisation
{
    public string Name { get; set; }
    public IEnumerable<Goal> Goals { get; set; }
    public IEnumerable<CurrencyAmount> TotalAmount { get; set; }
}
