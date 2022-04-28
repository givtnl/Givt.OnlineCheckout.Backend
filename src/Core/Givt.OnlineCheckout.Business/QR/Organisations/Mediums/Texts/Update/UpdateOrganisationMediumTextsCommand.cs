using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsCommand : LocalisableTextsCore, IConcurrency, IRequest<UpdateOrganisationMediumTextsResult>
{
    public long OrganisationId { get; set; }
    public string MediumId { get; set; }
    public string LanguageId { get; set; }
    public uint ConcurrencyToken { get; set; }
}
