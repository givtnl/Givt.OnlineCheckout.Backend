using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using integrations = Givt.OnlineCheckout.Integrations.Interfaces.Models;

namespace Givt.OnlineCheckout.Business.Extensions;

public static class OrganisationDataExtensions
{
    public static IEnumerable<integrations.PaymentMethod> GetPaymentMethods(this OrganisationData organisation)
    {
        if (organisation.PaymentMethods > 0)
            return organisation.PaymentMethods.MapPaymentMethods();

        if (organisation.Country?.PaymentMethods > 0)
            return organisation.Country?.PaymentMethods.MapPaymentMethods();
        return new List<PaymentMethod>();
    }

}