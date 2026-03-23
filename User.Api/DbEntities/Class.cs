using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Class
{
    public int Id { get; set; }

    public string ClassName { get; set; } = null!;

    public string Section { get; set; } = null!;

    public int ClassTeacherId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int Capacity { get; set; }

    public string AcademicYear { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public string? Subjects { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Teacher ClassTeacher { get; set; } = null!;
}