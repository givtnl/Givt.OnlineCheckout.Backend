using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Texts.Create;

public class CreateOrganisationTextsCommandHandler : IRequestHandler<CreateOrganisationTextsCommand, CreateOrganisationTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CreateOrganisationTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CreateOrganisationTextsResult> Handle(CreateOrganisationTextsCommand request, CancellationToken cancellationToken)
    {
        // TODO: validation
        var organisation = await _context.Organisations
            .Where(o => o.Id == request.OrganisationId)
            .FirstOrDefaultAsync(cancellationToken);
        if (organisation == null)
            throw new NotFoundException(nameof(OrganisationData), request); // TODO: name

        var data = _mapper.Map<OrganisationTexts>(request);
        data.Organisation = organisation;
        _context.Add(data);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateOrganisationTextsResult>(data);
    }
}