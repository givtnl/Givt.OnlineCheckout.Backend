using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;

namespace Givt.OnlineCheckout.Business.Donations;

public record CreateDonationIntentCommandHandler(ISinglePaymentService SinglePaymentService) : IRequestHandler<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
{
    public async Task<CreateDonationIntentCommandResponse> Handle(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var result = await SinglePaymentService.CreatePaymentIntent(
                request.Currency,
                request.Amount,
                request.Amount * (request.ApplicationFeePercentage / 100) + request.ApplicationFeeFixedAmount,
                request.Medium.Organisation.PaymentProviderAccountReference,
                request.PaymentMethod);

        return new CreateDonationIntentCommandResponse
        {
            PaymentIntentSecret = result.ClientToken,
            TransactionReference = result.TransactionReference
        };
    }
}