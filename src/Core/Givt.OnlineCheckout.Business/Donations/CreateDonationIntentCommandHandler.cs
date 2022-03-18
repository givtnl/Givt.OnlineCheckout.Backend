using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;

namespace Givt.OnlineCheckout.API.Donations;

public class CreateDonationIntentCommandHandler: IRequestHandler<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
{
    private readonly ISinglePaymentService _singlePaymentService;

    public CreateDonationIntentCommandHandler(ISinglePaymentService singlePaymentService)
    {
        _singlePaymentService = singlePaymentService;
    }
    
    public async Task<CreateDonationIntentCommandResponse> Handle(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var result = await _singlePaymentService.CreatePaymentIntent(request.Amount, request.Amount * 0.045m, request.AccountId, request.PaymentMethod);

        return new CreateDonationIntentCommandResponse
        {
            PaymentIntentSecret = result
        };
    }
}