using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR;

namespace Givt.OnlineCheckout.Application.Donations;

public class CreateDonationIntentCommandHandler: IRequestHandler<CreateDonationIntentCommand, CreateDonationIntentCommandResponse>
{
    private readonly ISinglePaymentService _singlePaymentService;

    public CreateDonationIntentCommandHandler(ISinglePaymentService singlePaymentService)
    {
        _singlePaymentService = singlePaymentService;
    }
    
    public Task<CreateDonationIntentCommandResponse> Handle(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}