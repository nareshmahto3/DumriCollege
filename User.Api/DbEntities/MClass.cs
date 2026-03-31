using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MClass
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();

    public virtual ICollection<Subject1> Subject1s { get; set; } = new List<Subject1>();
}
