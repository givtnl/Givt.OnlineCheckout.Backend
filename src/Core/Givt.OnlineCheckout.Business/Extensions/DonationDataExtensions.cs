﻿using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using Givt.OnlineCheckout.Persistance.Entities;
using System.Globalization;

namespace Givt.OnlineCheckout.Business.Extensions
{
    public static class DonationDataExtensions
    {
        public static string GetGoal(this DonationData donation, string language)
        {
            if (String.IsNullOrEmpty(language?.Replace('_', '-')?.Split('-')?.FirstOrDefault()))
                language = "en";

            return donation.Medium.GetLocalisedText(nameof(LocalisableTexts.Goal), language);
        }

        public static string GetTimestamp(this DonationData donation, string language)
        {
            if (String.IsNullOrEmpty(language?.Replace('_', '-')?.Split('-')?.FirstOrDefault()))
                language = "en";
            var cultureInfo = CultureInfo.GetCultureInfo(language);
            var timezoneOffset = new TimeSpan(0, -donation.TimezoneOffset, 0);
            string timezoneOffsetString =
                (donation.TimezoneOffset > 0) ? "-" : "+" + // yup, seems reversed but that's how it is. +00:00 for no offset from Z
                timezoneOffset.ToString(@"hh\:mm");
            return donation.TransactionDate.Value.Add(timezoneOffset).ToString(cultureInfo) + " " + timezoneOffsetString;
        }

        public static IEnumerable<Organisation> GetOrganisations(this DonationData donation, string language)
        {
            // no totals because it's just one donation
            /*
            var totals = new List<CurrencyAmount>
            {
                new CurrencyAmount { Currency = donation.Currency, Amount = donation.Amount.ToString("F2") }
            };
            */
            var organisations = new List<Organisation>();

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

            var donationInfo = new Donation
            {
                Amount = new CurrencyAmount { Currency = donation.Currency, Amount = donation.Amount.ToString("F2") },
                Timestamp = GetTimestamp(donation, language)
            };
            goal.Donations.Add(donationInfo);

            return organisations;
        }
    }
}