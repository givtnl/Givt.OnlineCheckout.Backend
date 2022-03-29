﻿using Givt.OnlineCheckout.Business.Models;

namespace Givt.OnlineCheckout.API.Models.Mediums;

public class GetMediumRequest
{
    public string Code { get; set;  }
    public string? Locale { get; set; }
    internal MediumIdType MediumId => new(Code);
}