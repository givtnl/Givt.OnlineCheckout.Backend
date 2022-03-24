using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

public record StripeEventHandler(ISinglePaymentService SinglePaymentService, OnlineCheckoutContext context) :
    IRequestHandler<PaymentProviderEventData, Unit>
{

    public async Task<Unit> Handle(PaymentProviderEventData request, CancellationToken cancellationToken)
    {
        // parse the incoming text blob to (a wrapper to) the Stripe Event Data object
        var eventData = await SinglePaymentService.GetEventData(request.Stream, request.MetaData);
        if (eventData == null)
            return Unit.Value;

        // load the matching donation
        var donation = await context.Donations
            .Where(d => d.TransactionReference == eventData.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
            return Unit.Value; // donation not found

        // update donation status
        if (eventData.Succeeded)
            donation.Status = DonationStatus.Succeeded;
        else if (eventData.Cancelled)
            donation.Status = DonationStatus.Cancelled;

        // write changes
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}