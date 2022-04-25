using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Mediums.Check;

public class CheckMediumQueryHandler : IRequestHandler<CheckMediumQuery, bool>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CheckMediumQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(CheckMediumQuery request, CancellationToken cancellationToken)
    {
        return await _context.Mediums
            .AnyAsync(x => x.Medium == request.MediumId.ToString(), cancellationToken);
    }
}