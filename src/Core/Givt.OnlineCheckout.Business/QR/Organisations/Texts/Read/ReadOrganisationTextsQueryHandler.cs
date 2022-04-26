using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Read;

public class ReadOrganisationTextsQueryHandler : IRequestHandler<ReadOrganisationTextsQuery, ReadOrganisationTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ReadOrganisationTextsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadOrganisationTextsResult> Handle(ReadOrganisationTextsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Organisations
            .Where(o => o.Id == request.OrganisationId)
            .SelectMany(o => o.Texts).Where(t => t.LanguageId == request.LanguageId)
            .FirstOrDefaultAsync(cancellationToken);
        return _mapper.Map<ReadOrganisationTextsResult>(data);
    }
}