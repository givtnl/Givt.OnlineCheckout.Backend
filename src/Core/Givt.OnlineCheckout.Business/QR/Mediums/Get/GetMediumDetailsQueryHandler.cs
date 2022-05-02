using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Business.QR.Mediums.Check;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Mediums.Get;

public class GetMediumDetailsQueryHandler : IRequestHandler<GetMediumDetailsQuery, MediumFlattenedModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public GetMediumDetailsQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<MediumFlattenedModel> Handle(GetMediumDetailsQuery request, CancellationToken cancellationToken)
    {
        var medium = await _context.Mediums
            .Where(x => x.Medium == request.MediumId.ToString())
            .Include(m => m.Texts)
            .Include(m => m.Organisation)
            .ThenInclude(o => o.Country)
            .SingleOrDefaultAsync(cancellationToken);
        if (medium == null)
            throw new NotFoundException(nameof(MediumIdType), request.MediumId);

        return _mapper.Map<MediumFlattenedModel>(medium, opt => { opt.Items["Language"] = request.Language; });
    }
}