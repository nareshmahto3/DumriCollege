using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MExamType
{
    public int ExamId { get; set; }

    public string ExamName { get; set; } = null!;

    public string? ExamType { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
