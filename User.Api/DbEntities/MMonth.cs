using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MMonth
{
    public int Id { get; set; }

    public string? MonthName { get; set; }

    public int? Year { get; set; }
}
