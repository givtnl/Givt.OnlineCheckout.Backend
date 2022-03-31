namespace Givt.OnlineCheckout.Business.Models.Report;

public class Organisation
{
    public string Name { get; set; }
    public IEnumerable<Goal> Goals { get; set; }
    public IEnumerable<CurrencyAmount> TotalAmount { get; set; }
}
