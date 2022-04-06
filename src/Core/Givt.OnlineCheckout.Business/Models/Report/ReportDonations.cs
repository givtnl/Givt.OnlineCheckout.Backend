using Givt.OnlineCheckout.Persistance.Entities;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.Models.Report;

public class ReportDonations
{
    // requested report langage / region
    public string Locale { get; set; }

    // donor info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    // donations
    public IEnumerable<Organisation> Organisations { get; set; }

    public static ReportDonations CreateFromDonation(DonationData donation, string language)
    {
        if (String.IsNullOrEmpty(language?.Split('-')?.FirstOrDefault()))
            language = "en";
        var cultureInfo = CultureInfo.GetCultureInfo(language);

        var result = new ReportDonations
        {
            Locale = language,
            /*
            FirstName = "FirstName_Value",
            LastName = "LastName_Value",
            Street = "Street_Value",
            PostalCode = "PostalCode_Value",
            City = "City_Value",
            Country = "Country_Value",
            */
        };
        // no totals because it's just one donation
        /*
        var totals = new List<CurrencyAmount>
        {
            new CurrencyAmount { Currency = donation.Currency, Amount = donation.Amount.ToString("F2") }
        };
        */
        var organisations = new List<Organisation>();
        result.Organisations = organisations;
        var organisation = new Organisation
        {
            Name = donation.Medium.Organisation.Name,
            //TotalAmount = totals
        };
        organisations.Add(organisation);
        var goals = new List<Goal>();
        organisation.Goals = goals;
        var goal = new Goal
        {
            Name = donation.Medium.Goal,
            //TotalAmount = totals,
            Donations = new List<Donation>()
        };
        goals.Add(goal);

        var timezoneOffset = new TimeSpan(0, -donation.TimezoneOffset, 0);
        string timezoneOffsetString =
            (donation.TimezoneOffset > 0) ? "-" : "+" + // yup, seems reversed but that's how it is. +00:00 for no offset from Z
            timezoneOffset.ToString(@"hh\:mm");
        var donationInfo = new Donation
        {
            Amount = new CurrencyAmount { Currency = donation.Currency, Amount = donation.Amount.ToString("F2") },
            Timestamp = donation.TransactionDate.Value.Add(timezoneOffset).ToString(cultureInfo) + " " + timezoneOffsetString

        };
        goal.Donations.Add(donationInfo);

        return result;
    }
}
