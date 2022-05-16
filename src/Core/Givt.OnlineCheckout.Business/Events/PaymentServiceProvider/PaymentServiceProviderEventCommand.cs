using MediatR;
using Microsoft.AspNetCore.Http;

namespace Givt.OnlineCheckout.Business.Events.PaymentServiceProvider;

public record PaymentServiceProviderEventCommand(string Body, IHeaderDictionary Headers) : IRequest;