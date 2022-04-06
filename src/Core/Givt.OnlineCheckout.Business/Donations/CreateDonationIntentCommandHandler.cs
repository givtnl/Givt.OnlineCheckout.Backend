using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;

namespace Givt.OnlineCheckout.Business.Donations;

public record GetDonationReportCommandHandler(ISinglePaymentService SinglePaymentService) : IRequestHandler<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
{
    public async Task<CreateDonationIntentCommandResponse> Handle(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var result = await SinglePaymentService.CreatePaymentIntent(
                request.Currency,
                request.Amount,
                request.Amount * 0.045m,
                request.Medium.Organisation.PaymentProviderAccountReference,
                request.PaymentMethod);

        return new CreateDonationIntentCommandResponse
        {
            PaymentIntentSecret = result.ClientToken,
            TransactionReference = result.TransactionReference
        };
    }
}