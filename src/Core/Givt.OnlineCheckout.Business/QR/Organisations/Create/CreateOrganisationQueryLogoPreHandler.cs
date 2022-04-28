using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR.Pipeline;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Create;

public class CreateOrganisationQueryLogoPreHandler : IRequestPreProcessor<CreateOrganisationQuery>
{
    private readonly IFileStorage _fileStorage;

    public CreateOrganisationQueryLogoPreHandler(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }
    public async Task Process(CreateOrganisationQuery request, CancellationToken cancellationToken)
    {
        var imageBytes = Convert.FromBase64String(request.LogoImageLink);
        await using var ms = new MemoryStream(imageBytes);
        await _fileStorage.UploadFile("public", $"cdn/goc-logo/{request.PaymentProviderAccountReference}.png", ms, null, cancellationToken);
        request.LogoImageLink = $"https://givtstorage.blob.core.windows.net/public/cdn/goc-logo/{request.PaymentProviderAccountReference}.png";
    }
}