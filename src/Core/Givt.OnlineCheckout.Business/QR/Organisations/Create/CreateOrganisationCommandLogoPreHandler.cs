using Givt.OnlineCheckout.Integrations.Interfaces;
using MediatR.Pipeline;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Create;

public class CreateOrganisationCommandLogoPreHandler : IRequestPreProcessor<CreateOrganisationCommand>
{
    private readonly IFileStorage _fileStorage;

    public CreateOrganisationCommandLogoPreHandler(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }
    public async Task Process(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        // TODO: validation
        var imageBytes = Convert.FromBase64String(request.LogoImageLink);
        await using var ms = new MemoryStream(imageBytes);
        await _fileStorage.UploadFile("public", $"cdn/goc-logo/{request.PaymentProviderAccountReference}.png", ms, null, cancellationToken);
        request.LogoImageLink = $"https://givtstorage.blob.core.windows.net/public/cdn/goc-logo/{request.PaymentProviderAccountReference}.png";
    }
}