namespace Givt.OnlineCheckout.DataAccess.DataModels;

public class DataOrganisation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PaymentProviderAccountReference { get; set; }
    public string Namespace { get; set; }
}