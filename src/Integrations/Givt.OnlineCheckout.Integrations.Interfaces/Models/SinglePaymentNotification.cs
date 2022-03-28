using MediatR;

namespace Givt.OnlineCheckout.Integrations.Interfaces.Models;

public abstract class SinglePaymentNotification : INotification
{
    public abstract string TransactionReference { get; }
    public abstract bool Processing { get; }
    public abstract bool Succeeded { get; }
    public abstract bool Cancelled { get; }
    public abstract bool Failed { get; }
}
