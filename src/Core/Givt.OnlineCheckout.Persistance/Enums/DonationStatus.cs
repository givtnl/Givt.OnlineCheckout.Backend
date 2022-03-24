namespace Givt.OnlineCheckout.Persistance.Enums
{
    public enum DonationStatus : byte
    {
        Created = 0,
        Processing = 1,
        Succeeded = 2,
        PaymentFailed = 3,
        Cancelled = 4,
        Revoked = 5, // e.g. direct debit reversal
    }
}
