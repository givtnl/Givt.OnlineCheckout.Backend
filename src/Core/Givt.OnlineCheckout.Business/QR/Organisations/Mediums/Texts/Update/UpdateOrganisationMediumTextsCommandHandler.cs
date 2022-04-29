using AutoMapper;
using Givt.OnlineCheckout.Business.Exceptions;
using Givt.OnlineCheckout.Infrastructure.DbContexts;
using Givt.OnlineCheckout.Persistance.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Mediums.Texts.Update;

public class UpdateOrganisationMediumTextsCommandHandler : IRequestHandler<UpdateOrganisationMediumTextsCommand, UpdateOrganisationMediumTextsResult>
{
    private readonly IMapper _mapper;
    private readonly OnlineCheckoutContext _context;

    public UpdateOrganisationMediumTextsCommandHandler(IMapper mapper, OnlineCheckoutContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UpdateOrganisationMediumTextsResult> Handle(UpdateOrganisationMediumTextsCommand request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<MediumTexts>()
            .Where(t => t.Medium.Medium == request.MediumId && t.LanguageId == request.LanguageId)
            .FirstOrDefaultAsync(cancellationToken);
        if (data == null)
            throw new NotFoundException(nameof(MediumTexts), request);
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
        return _mapper.Map<UpdateOrganisationMediumTextsResult>(data);
    }
}