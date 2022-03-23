using MediatR;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.API.Models;

namespace Givt.OnlineCheckout.API.Donations;

public class CreateDonationIntentCommand : IRequest<CreateDonationIntentCommandResponse>
{
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public MediumIdType MediumId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool TaxReportRequested { get; set; }
    public string Email { get; set; } // TODO: check/limit to 254 char (RFC 2821), validate email address structure and TLD??
    internal string AccountId { get; set; }
}