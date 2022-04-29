using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Read;

public class ReadOrganisationMediumTextsQueryHandler : IRequestHandler<ReadOrganisationMediumTextsQuery, ReadOrganisationMediumTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ReadOrganisationMediumTextsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadOrganisationMediumTextsResult> Handle(ReadOrganisationMediumTextsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Mediums
            .Where(m => m.OrganisationId == request.OrganisationId && m.Medium == request.MediumId)
            .SelectMany(m => m.Texts).Where(t => t.LanguageId == request.LanguageId)
            .FirstOrDefaultAsync(cancellationToken);
        return _mapper.Map<ReadOrganisationMediumTextsResult>(data);
    }
}