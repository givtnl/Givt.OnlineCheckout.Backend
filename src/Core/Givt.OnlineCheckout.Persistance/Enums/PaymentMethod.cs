namespace Givt.OnlineCheckout.Persistance.Enums;

[Flags]
public enum PaymentMethod : UInt64
{
    Bancontact  = 0x0000000000000001, // Bank redirect. BE / EUR
    Card        = 0x0000000000000002, // credit or debit. Global, 135+ currencies
    Ideal       = 0x0000000000000004, // Bank redirect. NL / EUR
    Sofort      = 0x0000000000000008, // Bank redirect. AT, BE, DE, IT, NL, ES / EUR (acquired by Klarna)
    Giropay     = 0x0000000000000010, // Bank redirect. DE / EUR
    EPS         = 0x0000000000000020, // Bank redirect. AT / EUR
    ApplePay    = 0x0000000000000040, // "global", 135+ currencies
    GooglePay   = 0x0000000000000080, // "global", 135+ currencies
    All = 0xFFFFFFFFFFFFFFFF
}
// Other stuff supported by Stripe:

//MicrosoftPay// 135+ currencies
//bacs_debit  // Bank debit. UK / GBP
// acss_debit 
// afterpay_clearpay - Afterpay / Clearpay. Buy now, pay later: Australia, Canada, France, Italy, New Zealand, Spain, United Kingdom, United States. AUD, CAD, EUR, GBP, NZD, USD
// alipay: Wallet. All geographies with Chinese consumers. AUD, CAD, CNY, EUR, GBP, HKD, JPY, MYR, NZD, USD
// au_becs_debit. BECS Direct Debit (Bulk Electronic Clearing System). Bank debit, AU / AUD
// boleto. Cash-based voucher. Brazil / BRL
// Click to Pay. Wallet, Global, 135+ currencies
// fpx. Financial Process Exchange. Malaysia / MYR
// Grab Pay. Digital wallet. Singapore, Malaysia / SGD, MYR
// Klarna. Buy now, pay later. Austria, Belgium, Denmark, Finland, Germany, Italy, Norway, Spain, Sweden, The Netherlands, UK, US / EUR, GBP, USD
// Multi Banco. Bank credit transfer. Portugal / EUR
// card_present 
// customer_balance 
// interac_present 
// konbini 
// oxxo. Mexico / MXN
// p24 Przelewy24. Bank redirect. Poland / PLN
// paynow 
// sepa_debit SEPA Direct Debit
// us_bank_account 
// wechat pay. Wallet. All geographies with Chinese consumers / AUD, CAD, EUR, GBP, HKD, JPY, SGD, USD
// ACH Credit Transfers (ACH = Automated Clearing House) - US / USD
// ACH Debits - US / USD
// Affirm - US / USD (Buy now, pay later)
