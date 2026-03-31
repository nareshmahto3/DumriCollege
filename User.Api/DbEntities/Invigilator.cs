using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Invigilator
{
    public int InvigilatorId { get; set; }

    public string? Name { get; set; }

    public string? Mobile { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();
}
