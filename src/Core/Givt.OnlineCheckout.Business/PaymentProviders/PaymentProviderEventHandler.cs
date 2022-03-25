using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.PaymentProviders;

public record PaymentProviderEventHandler(ISinglePaymentService SinglePaymentService, OnlineCheckoutContext Context) :
    IRequestHandler<PaymentProviderEventData, Unit>
{
    public async Task<Unit> Handle(PaymentProviderEventData request, CancellationToken cancellationToken)
    {
        // parse the incoming text blob to the Event Data object
        var eventData = SinglePaymentService.GetEventData(request.Content, request.MetaData);
        if (eventData == null)
            return Unit.Value;

        // load the matching donation
        var donation = await Context.Donations
            .Where(d => d.TransactionReference == eventData.TransactionReference)
            .FirstOrDefaultAsync(cancellationToken);
        if (donation == null)
            return Unit.Value; // donation not found

        // update donation status
        if (eventData.Processing)
            donation.Status = DonationStatus.Processing;
        else if (eventData.Succeeded)
            donation.Status = DonationStatus.Succeeded;
        else if (eventData.Cancelled)
            donation.Status = DonationStatus.Cancelled;
        else if (eventData.Failed)
            donation.Status = DonationStatus.PaymentFailed;

        // write changes
        await Context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}