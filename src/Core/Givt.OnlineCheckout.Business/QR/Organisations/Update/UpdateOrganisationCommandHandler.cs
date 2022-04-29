using AutoMapper;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Update;

public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, OrganisationDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrganisationDetailModel> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
    {
        // TODO: validation
        var data = _mapper.Map<OrganisationData>(request);
        _context.Add(data).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OrganisationDetailModel>(data);
    }
}