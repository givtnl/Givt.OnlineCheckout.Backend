using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
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
        var data = await _context.Organisations
            .Where(o => o.Id == request.OrganisationId)
            .SelectMany(o => o.Texts)
            .Where(t => t.LanguageId == request.LanguageId)
            .FirstOrDefaultAsync(cancellationToken);
        if (data == null)
            throw new NotFoundException(nameof(OrganisationTexts), request);
        _mapper.Map(request, data); // update data (merge into persistance object)
        try
        {
            var count = await _context.SaveChangesAsync(cancellationToken);
            if (count == 0)
                throw new ConcurrentUpdateException();
        }
        catch (DbUpdateConcurrencyException duce)
        {
            Console.WriteLine(duce.Message);
            throw new ConcurrentUpdateException();
        }
        return _mapper.Map<UpdateOrganisationTextsResult>(data);
    }
}