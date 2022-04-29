namespace Givt.OnlineCheckout.Business.Models;

public interface IConcurrency
{
    public uint ConcurrencyToken { get; set; }
}
