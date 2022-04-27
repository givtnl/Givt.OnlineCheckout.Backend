using AutoMapper;
using Givt.OnlineCheckout.Business.Extensions;
using Givt.OnlineCheckout.Business.Models;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Create;

public class CreateOrganisationQueryHandler : IRequestHandler<CreateOrganisationQuery, OrganisationModel>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public CreateOrganisationQueryHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OrganisationModel> Handle(CreateOrganisationQuery request, CancellationToken cancellationToken)
    {
        // data validation
        if (String.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentNullException(nameof(request.Name));
        request.Name = request.Name.Trim();
        if (request.Name.Length > 35)
            throw new ArgumentOutOfRangeException(nameof(request.Name), "too long");
        if (String.IsNullOrWhiteSpace(request.Namespace))
            throw new ArgumentNullException(nameof(request.Namespace));
        var country = await _context.Countries
            .Where(c => c.CountryCode == request.Country.ToUpperInvariant())
            .FirstOrDefaultAsync(cancellationToken);
        if (country == null)
            throw new ArgumentException(nameof(request.Country));

        // store and return
        var data = _mapper.Map<OrganisationData>(request);
        _context.Organisations.Add(data);
        var res = _context.SaveChanges();
        // TODO: remove this hack. 
        data.Country = null; // Reset to return actual PaymentMethods on the ORganisation Object
        return _mapper.Map<OrganisationModel>(data);
    }
}