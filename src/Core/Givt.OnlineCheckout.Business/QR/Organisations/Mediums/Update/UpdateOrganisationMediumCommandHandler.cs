using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Update;

public class UpdateOrganisationMediumCommandHandler : IRequestHandler<UpdateOrganisationMediumCommand, MediumDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationMediumCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<MediumDetailModel> Handle(UpdateOrganisationMediumCommand request, CancellationToken cancellationToken)
    {
        // TODO: validation
        var medium = await _context.Mediums
            .Where(o => o.Medium == request.Medium)
            .FirstOrDefaultAsync(cancellationToken);
        if (medium == null)
            throw new NotFoundException(nameof (MediumData), request.Medium);
        
        _mapper.Map(request, medium);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MediumDetailModel>(medium);
    }
}