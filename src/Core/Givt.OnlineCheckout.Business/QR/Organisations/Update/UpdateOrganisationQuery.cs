﻿using Givt.OnlineCheckout.Business.Models;
using MediatR;

namespace Givt.OnlineCheckout.Business.QR.Organisations.Update;

public class UpdateOrganisationQuery: OrganisationModel, IRequest<OrganisationModel>
{
}
