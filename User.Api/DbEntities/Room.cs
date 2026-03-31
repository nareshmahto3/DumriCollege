using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Room
{
    public int RoomId { get; set; }

    public string? RoomName { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();
}
