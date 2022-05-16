namespace Givt.OnlineCheckout.Integrations.Interfaces.Models
{
    public class GivtInfo
    {
        /*
		"Name": "Givt B.V.",
		"Address": "Schutweg 47, 8243 PC Lelystad",
		"Email": "support@givtapp.net",
		"PhoneNumber": "+31 320 320 115",
		"Website": "www.givtapp.net"
		*/
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }
}