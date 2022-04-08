using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Extensions
{
    public static class MediumDetailModelExtensions
    {
        public static int[] MapPaymentMethods(this MediumDetailModel model)
        {
            var businessPaymentMethods = (UInt64)model.PaymentMethods;
            var apiPaymentMethods = new List<int>();
            UInt64 mask = 0x0000000000000001;
            for (int i = 0; i <= 63; i++)
            {
                if ((businessPaymentMethods & mask) != 0) { apiPaymentMethods.Add(i); }
                mask = mask << 1;
            }
            return apiPaymentMethods.ToArray();
        }
    }
}
