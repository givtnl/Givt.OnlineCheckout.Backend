﻿using MediatR;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Application.Donations;

public class CreateDonationIntentCommand: IRequest<CreateDonationIntentCommandResponse>
{
    public decimal Amount { get; set; }
    public string Medium { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    internal string AccountId { get; set; }
}