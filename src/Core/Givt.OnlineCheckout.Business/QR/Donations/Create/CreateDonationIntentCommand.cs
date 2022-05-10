using MediatR;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Persistance.Entities;

namespace Givt.OnlineCheckout.Business.QR.Donations.Create;

public class CreateDonationIntentCommand : IRequest<CreateDonationIntentCommandResponse>
{    
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public decimal ApplicationFeePercentage { get; set; }
    public decimal ApplicationFeeFixedAmount { get; set; }
    public string Description { get; set; }
    public MediumIdType MediumId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Language { get; set; }
    public int TimezoneOffset { get; set; }
    internal MediumData Medium { get; set; }
}