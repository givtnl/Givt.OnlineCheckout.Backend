using MediatR;

namespace Givt.OnlineCheckout.Integrations.Interfaces;

public interface ISinglePaymentNotification : INotification
{
    string TransactionReference { get; }
    DateTime? TransactionDate { get; }
    string PaymentMethod { get; }
    string Fingerprint { get; }
    bool Processing { get; }
    bool Succeeded { get; }
    bool Cancelled { get; }
    bool Failed { get; }
}
