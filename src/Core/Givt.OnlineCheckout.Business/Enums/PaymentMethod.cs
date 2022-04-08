namespace Givt.OnlineCheckout.Business.Enums;

[Flags]
public enum PaymentMethod : UInt64
{
    Card        = 0x0000000000000001, 
    Ideal       = 0x0000000000000002, 
    Bancontact  = 0x0000000000000004, 
    
    Sofort      = 0x0000000000000020, 
    
    ApplePay    = 0x0000000000010000, 
    GooglePay   = 0x0000000000020000,

    All         = 0xFFFFFFFFFFFFFFFF,
}
