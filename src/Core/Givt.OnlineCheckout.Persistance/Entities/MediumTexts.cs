namespace Givt.OnlineCheckout.Persistance.Entities
{
    public class MediumTexts
    {
        public uint ConcurrencyToken { get; set; }
        public string LanguageId { get; set; } // length at least 14 (ca-ES-valencia = catalan spain valencia dialect)
        public long MediumId { get; set; }
        public MediumData Medium { get; set; }
        public string Title { get; set; } // campaign title
        public string Goal { get; set; }
        public string ThankYou { get; set; }
    }
}
