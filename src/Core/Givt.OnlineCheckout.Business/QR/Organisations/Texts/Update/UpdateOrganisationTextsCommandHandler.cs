using AutoMapper;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Update;

public class UpdateOrganisationTextsCommandHandler : IRequestHandler<UpdateOrganisationTextsCommand, UpdateOrganisationTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UpdateOrganisationTextsResult> Handle(UpdateOrganisationTextsCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<OrganisationTexts>(request);
        _context.Add(data).State= EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateOrganisationTextsResult>(data);
    }
}