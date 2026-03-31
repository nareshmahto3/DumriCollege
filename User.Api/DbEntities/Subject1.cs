using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Subject1
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int? ClassId { get; set; }

    public virtual MClass? Class { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();
}
