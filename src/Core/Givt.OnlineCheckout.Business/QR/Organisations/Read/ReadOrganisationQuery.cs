﻿using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Read;

public class ReadOrganisationQuery : IRequest<OrganisationModel>
{
    public long OrganisationId { get; set; }
}
