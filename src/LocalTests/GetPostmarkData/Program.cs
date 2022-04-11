using Givt.OnlineCheckout.Business.Models.Report;
using Givt.OnlineCheckout.Integrations.Postmark;
using Givt.OnlineCheckout.Persistance.Entities;
using Givt.OnlineCheckout.Persistance.Enums;
using Newtonsoft.Json;

const string S_GOAL = "A medium goal, not so special";
const string S_THANKYOU = "Thanks mate";

var now = DateTime.UtcNow;

// setup a full donation
var organisation = new OrganisationData
{
    Id = 890,
    DateCreated = now,
    DateModified = now,
    Active = true,
    Currency = "EUR",
    Name = "Test Organisation",
    Namespace = "61f7xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    PaymentProviderAccountReference = "acct_1Kc3zNQ9aZIOSVFA",
    Texts = new List<OrganisationTexts>(),
};

var medium = new MediumData
{
    Id = 234567,
    DateCreated = now,
    DateModified = now,
    Amounts = "3.5,5.5,9",
    Goal = S_GOAL, // will be removed
    ThankYou = S_THANKYOU, // will be removed
    Texts = new List<MediumTexts>(),
    Medium = organisation.Namespace + '.' + "c0000007",
    OrganisationId = organisation.Id,
    Organisation = organisation,
};

medium.Texts.Add(
    new MediumTexts
    {
        //Medium = medium,
        Goal = S_GOAL,
        ThankYou = S_THANKYOU,
    }
);

var donor = new DonorData
{
    Id = Guid.NewGuid(),
    DateCreated = now,
    DateModified = now,
    Email = "test@givtapp.net",
    Donations = new List<DonationData>()
};

var donation = new DonationData
{
    Id = 12345,
    DateCreated = now,
    DateModified = now,
    Currency = "EUR",
    Amount = 15.75m,
    TransactionReference = "pi_3KifAdLgFatYzb8p3tULEGG6",
    TransactionDate = now,
    TimezoneOffset = -120,
    LanguageId = "en-GB",
    Medium = medium,
    Status = DonationStatus.Succeeded,
};
// link donor to donation vv.
donation.Donor = donor;
//donor.Donations.Add(donation);

var report = ReportDonations.CreateFromDonation(donation);

var transferData = JsonConvert.SerializeObject(report, Formatting.Indented, new LocaleConverter(typeof(ReportDonations)));

Console.WriteLine(transferData);
