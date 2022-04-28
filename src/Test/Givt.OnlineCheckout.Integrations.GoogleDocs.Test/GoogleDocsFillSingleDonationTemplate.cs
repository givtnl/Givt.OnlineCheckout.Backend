using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Givt.OnlineCheckout.Integrations.Interfaces.Models;
using NUnit.Framework;

namespace Givt.OnlineCheckout.Integrations.GoogleDocs.Test;

public class Tests
{
    private GoogleDocsOptions _options = new GoogleDocsOptions();

    [SetUp]
    public void Setup()
    {        
        _options.ApplicationName = "Givt API";
        _options.ServiceAccountEmail = "givtapidocuments@quickstart-1568214171869.iam.gserviceaccount.com";
        _options.PrivateKey =
                "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC/38SPdqXlkZGt\ncxb7JNvc9t2TsI7wYWdoRXby3eWp9jGWucgnZsvfEF3YTL8d2mUGVjfhPxwOTHYn\nh7VCfa1vWRxE7Zfuu3cfxktYbIBiVW58K/8O13Wxg6kEWTxYgl86/4FRO0U81mhy\n0n9ydunJCwYoy2kPIKhElIcJHIH6pwVlhzC5o7Tzx4x/iYZk3L6/schyzYTPmBks\nbbFuZJhcGKTtyIW+4TkokpsVpEPd7FR8LN5vUqdutuQ1+UaHXqAJdjqtmZo8z/A2\nq3yWotIzEzKowMM7+BZhsOuAvS1JR7+0C7VbkJxDJDtMg648+BpgZAAnbFL/hSbW\n4XPXr0dtAgMBAAECggEAQSVtPBaZSiJMN3uBnIDNw67v2YLTbWts7DrVoa1Umon0\nGUx32GTvKIwKiPO52h16OpYQgAJo+LPeVBqgIPTB67HyRrby5IQy9I9jqqCgUILY\nMgHQJk5icASXYNoZgqW1RrdUxWtd3UOhEAdHbWpRg0iibTxvLcs1Rp+1X5MV0K/H\ntt5/rvlXkB9iGgIUKegQ46DShldCAE/IgPFCP3GRZlgZIwZNGXwAMQu0nf/49Lwj\nD00Uxo+6mR0QAYc2gQUBgrwfqZB2Opq/iJWIARfUw6050CIn1Wq5Zuy+D+MriCJ5\nXlBXCWnMwsXV8pitleiFGVXe0kfYNJqd52hWXtITwwKBgQD/5Whob8crNFxUMQah\n7KLDdiF/Zsp14NLJ4jDdKtV/RLB+6oc8xqduo/T71Q0A4/rlEFLfxnEcTBLKElNx\nKrVEHADIvCqJ2uRAIW4aiEdAP+iq2IKW8zh3LpOqhUY9tgrBEZOtePvu8P5LyVLb\nGiNRwvJyrcu/OU05O6Moud5rLwKBgQC/87T6PLMb1LQSYGBRhLlO2zZlcGH4Jv3b\nr84at5xF2h7c5Gw5tf85QeIQAM6Ww4M7k0/AbyHmVgURvVsQJ5/GdDI5NedKvu0S\nk+4r0iG5tGs47SYuFXcLsEkJIWqZCzGEv8CGR7XQkiYE89g9ujjhprYIjVssqC/n\nR0orQftgIwKBgQChTZt90Y/rhQr6KxkyMrb6nNlKtKHEol5c0c7Zaym/Gq5iojVz\nMwnRrVo88DRNLmg1wg9rbWxZWP6zD9A3UqOGdlxsLGhoE2mznx8HbIX1UxO3tdjD\njDc4sUx1vaLkPX8T6p97RvsPyCuT3Cj1YcmHvWoUrcb0GAu8mwIjJt60EwKBgE+G\nmKRUIaCWL0StyTn80wloqNC+DtjQzrwFyk4nZAdMpxy9RllinasLMK2QqAauWeCE\n17XSVSko2lDPMrsur3N5EJXDT2AYWgAoabSeCnr11LZxCjBtlDSzhI7T2WYuzQVC\nrHD1pM4UvsuuexiX3pkeiKxiZDPMOoyHbKMfYxYhAoGADvIs82eOgGn5tbgm8ldf\nO1yi839Bxk5jQgOBGXw8wQe5Pl83o4Rlg9cpbb2YK2We+Qr2tcWqXGo8d3xqRHGQ\nVuSeU0RzypNiVqH19b+/yynK3fOGEAlOaGxYbA7soL0FgBotEy0slfcUf9r98BQH\nHrtvA0vmkBcz0OwTD7tYdDg=\n-----END PRIVATE KEY-----\n";
        _options.DonationConfirmationEN = "1f9R0qyODGDQ7Tk-jLLVfyi1OitfMo2S_Bknuqy8CgXA";
        _options.DonationConfirmationNL = "1YOxWZHP6Hw4ucw8SHBTsardYenjM9JETt3A8tWcYgzs";
    }

    [Test]
    public async Task Test1()
    {
        var service = new GooglePdfService(_options);
        var doc = await service.CreateSinglePaymentReport(new DonationReport()
        {
            OrganisationName = "Bjorns epic organisation",
            Timestamp = DateTime.Now,
            RSIN = "002871452",
            Goal = "Verzameling van gelden voor nieuwe vaatwasmachine",
            ThankYou = "Bedankt! Nu hoeven wij de vaat niet iedere dag opnieuw te doen!",
            PaymentMethod = "Google Pay",
            Currency = "EUR",
            Amount = 4.4M,
        }, new CultureInfo("nl-NL"), CancellationToken.None);
        await File.WriteAllBytesAsync("c:\\Users\\bjorn\\hello_EN.pdf", doc.Content);
    }
}