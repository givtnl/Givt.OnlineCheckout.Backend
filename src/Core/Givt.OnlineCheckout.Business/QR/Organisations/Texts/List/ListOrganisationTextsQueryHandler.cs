using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.List;

public class ListOrganisationTextsQueryHandler : IRequestHandler<ListOrganisationTextsQuery, List<LocalisableTextModel>>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public ListOrganisationTextsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<LocalisableTextModel>> Handle(ListOrganisationTextsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Organisations
            .Where(x => x.Id == request.OrganisationId)
            .SelectMany(x => x.Texts)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LocalisableTextModel>>(data);
    }
}