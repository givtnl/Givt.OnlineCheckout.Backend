﻿using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Organisations;

public class LocalisableTextsResponse: LocalisableTextsCore
{
    public uint ConcurrencyToken { get; set; }
    public string LanguageId { get; set; }
}