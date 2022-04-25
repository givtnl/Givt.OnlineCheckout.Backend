using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using integrations = Givt.OnlineCheckout.Integrations.Interfaces.Models;
using persistance = Givt.OnlineCheckout.Persistance.Enums;

namespace Givt.OnlineCheckout.Business.Extensions
{
    public static class PaymentMethodExtensions
    {
        public static IEnumerable<integrations.PaymentMethod> MapPaymentMethods(this persistance.PaymentMethod paymentMethods)
        {
            var persistancePaymentMethods = (UInt64)paymentMethods;
            var result = new List<integrations.PaymentMethod>();
            UInt64 mask = 0x0000000000000001;
            for (int i = 0; i < sizeof(persistance.PaymentMethod) * 8; i++)
            {
                if ((persistancePaymentMethods & mask) != 0) { result.Add((integrations.PaymentMethod)i); }
                mask <<= 1;
            }
            return result;
        }

        public static persistance.PaymentMethod MapPaymentMethods(this IEnumerable<integrations.PaymentMethod> paymentMethods)
        {
            UInt64 result = 0;
            UInt64 mask = 0x0000000000000001;
            foreach (var paymentMethod in paymentMethods)
                result |= mask << (byte)paymentMethod;

            return (persistance.PaymentMethod)result;
        }

        public static persistance.PaymentMethod MapPaymentMethods(this IEnumerable<string> paymentMethods)
        {
            UInt64 result = 0;
            foreach (var paymentMethod in paymentMethods)
            {
                var ppm = (persistance.PaymentMethod)Enum.Parse(typeof(persistance.PaymentMethod), paymentMethod, true);
                result |= (UInt64)ppm;
            }
            return (persistance.PaymentMethod)result;
        }

    }

}
