using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class ExamSchedule
{
    public int ScheduleId { get; set; }

    public int? ExamId { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public DateOnly? ExamDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int? RoomId { get; set; }

    public int? InvigilatorId { get; set; }

    public virtual MClass? Class { get; set; }

    public virtual MExamType? Exam { get; set; }

    public virtual Invigilator? Invigilator { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Subject1? Subject { get; set; }
}
