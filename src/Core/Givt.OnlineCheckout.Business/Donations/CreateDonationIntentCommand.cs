using MediatR;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.API.Models;

namespace Givt.OnlineCheckout.API.Donations;

public class CreateDonationIntentCommand: IRequest<CreateDonationIntentCommandResponse>
{
    public decimal Amount { get; set; }
    public MediumIdType MediumId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    internal string AccountId { get; set; }
}