using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Givt.OnlineCheckout.Business.QR.Donations.Create;

public record CreateDonationIntentFetchAccountIdPreHandler(OnlineCheckoutContext DbContext) : IRequestPreProcessor<CreateDonationIntentCommand>
{
    public async Task Process(CreateDonationIntentCommand request, CancellationToken cancellationToken)
    {
        var medium = await DbContext.Mediums
            .Where(x => x.Medium == request.MediumId.ToString())
            .Include(x => x.Texts)
            .Include(x => x.Organisation)
            .FirstOrDefaultAsync(cancellationToken);

        if (medium == null)
            throw new NotFoundException(nameof(MediumData), request.MediumId);

        if (medium.Organisation.PaymentProviderAccountReference == null)
            throw new BadRequestException("Organisation has no account reference");

        request.Medium = medium;

        var description = new StringBuilder();
        description.Append(medium.GetLocalisedText(nameof(MediumTexts.Title), request.Language)?.Trim());
        if (description.Length > 0)
            description.Append(" - ");
        description.Append(medium.Organisation.Name);

        request.Description = description.ToString();
    }
}