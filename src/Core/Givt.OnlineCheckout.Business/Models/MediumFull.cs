namespace Givt.OnlineCheckout.Business.Models;

public class MediumFull : MediumCoreModel, IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}