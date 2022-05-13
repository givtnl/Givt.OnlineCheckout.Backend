using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Integrations.Interfaces;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Http.Logger;
using PaymentMethod = Givt.OnlineCheckout.Persistance.Enums.PaymentMethod;

namespace Givt.OnlineCheckout.Business.Events.PaymentServiceProvider;

public record PaymentServiceProviderEventCommandHandler(IEnumerable<ISinglePaymentService> SinglePaymentServices, OnlineCheckoutContext DbContext, ILog Log) : IRequestHandler<PaymentServiceProviderEventCommand>
{
    public async Task<Unit> Handle(PaymentServiceProviderEventCommand request, CancellationToken cancellationToken)
    {
        var singlePaymentService = SinglePaymentServices.Single(x => x.CanHandle(request.Headers));
        var notification = singlePaymentService.ConstructNotification(request.Body, request.Headers);

        if (!notification.Succeeded)
            return Unit.Value;
        
        var donation = await DbContext.Donations.SingleOrDefaultAsync(x => x.TransactionReference == notification.TransactionReference, cancellationToken);
        
        if (donation == null)
            throw new NotFoundException(nameof(Donation), notification);

        await UpdatePaymentStatus(notification, donation, cancellationToken);

        return Unit.Value;
    }
    
    private async Task UpdatePaymentStatus(ISinglePaymentNotification notification, DonationData donation, CancellationToken cancellationToken)
    {
        if (notification.Processing)
            donation.Status = DonationStatus.Processing;
        else if (notification.Cancelled)
            donation.Status = DonationStatus.Cancelled;
        else if (notification.Failed)
            donation.Status = DonationStatus.PaymentFailed;
        else if (notification.Succeeded)
            HandleNotificationSucceeded(notification, donation);
        
        await DbContext.SaveChangesAsync(cancellationToken);
        
        if (!notification.Processing)
            Log.Information("Donation with transaction reference '{0}' set to status {1}", new object[] { notification.TransactionReference, donation.Status });
    }
    
    private static void HandleNotificationSucceeded(ISinglePaymentNotification notification, DonationData donation)
    {
        if (donation.Status == DonationStatus.Succeeded) return;
        
        donation.Status = DonationStatus.Succeeded;
        donation.TransactionDate = notification.TransactionDate;
        donation.PaymentMethod = (PaymentMethod)notification.PaymentMethod;
        donation.Fingerprint = notification.Fingerprint;
    }
}