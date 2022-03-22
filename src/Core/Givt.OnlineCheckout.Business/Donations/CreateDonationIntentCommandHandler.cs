using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;

namespace Givt.OnlineCheckout.API.Donations;

public record CreateDonationIntentCommandHandler(ISinglePaymentService SinglePaymentService) : IRequestHandler<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
{
    public async Task<CreateDonationIntentCommandResponse> Handle(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var result = await SinglePaymentService.CreatePaymentIntent(request.Amount, request.Amount * 0.045m, request.AccountId, request.PaymentMethod);

        return new CreateDonationIntentCommandResponse
        {
            PaymentIntentSecret = result
        };
    }
}