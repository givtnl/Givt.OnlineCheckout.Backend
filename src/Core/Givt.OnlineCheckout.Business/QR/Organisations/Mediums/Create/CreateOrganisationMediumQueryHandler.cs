using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Create;

public class CreateOrganisationMediumQueryHandler : IRequestHandler<CreateOrganisationMediumQuery, MediumDetailModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CreateOrganisationMediumQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<MediumDetailModel> Handle(CreateOrganisationMediumQuery request, CancellationToken cancellationToken)
    {
        // data validation
        var organisation = await _context.Organisations
            .Where(o => o.Id == request.OrganisationId)
            .FirstOrDefaultAsync(cancellationToken);
        if (organisation == null)
            throw new NotFoundException("request.organisationId", request.OrganisationId);

        if (String.IsNullOrWhiteSpace(request.Medium))
            throw new ArgumentNullException(nameof(request.Medium));
        if (!request.Medium.StartsWith(organisation.Namespace))
            throw new ArgumentException("MediumID does not correspond with Organisation");
        var mediumId = new MediumIdType(request.Medium);

        if (request.Amounts.Length == 0)
            throw new ArgumentException("At least one amount is required");
        foreach (var amount in request.Amounts)
            if (amount <= 0)
                throw new ArgumentException("Amounts must be larger than zero");

        // store and return
        var data = _mapper.Map<MediumData>(request);
        _context.Mediums.Add(data);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MediumDetailModel>(data);
    }
}