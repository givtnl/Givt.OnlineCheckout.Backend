using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.List;

public class ListOrganisationMediumTextsQueryHandler : IRequestHandler<ListOrganisationMediumTextsQuery, List<LocalisableTextModel>>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ListOrganisationMediumTextsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<LocalisableTextModel>> Handle(ListOrganisationMediumTextsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Mediums
            .Where(x => x.OrganisationId == request.OrganisationId && x.Medium == request.MediumId)
            .SelectMany(x => x.Texts)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LocalisableTextModel>>(data);
    }
}