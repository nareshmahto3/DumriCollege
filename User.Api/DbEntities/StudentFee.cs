using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class StudentFee
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? MonthId { get; set; }

    public string? Status { get; set; }
}
