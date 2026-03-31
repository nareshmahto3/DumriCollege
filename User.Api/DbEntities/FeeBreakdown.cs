using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class FeeBreakdown
{
    public int Id { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }
}
