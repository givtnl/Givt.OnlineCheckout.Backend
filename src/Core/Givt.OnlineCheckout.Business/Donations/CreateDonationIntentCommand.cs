using MediatR;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.Business.Donations;

public class CreateDonationIntentCommand : IRequest<CreateDonationIntentCommandResponse>
{
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public MediumIdType MediumId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool TaxReport { get; set; }
    public string Email { get; set; }
    public int? TimezoneOffset { get; set; } // TODO: make this a required parameter
    internal string AccountId { get; set; }
}