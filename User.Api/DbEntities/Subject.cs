using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectName { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public int Credits { get; set; }

    public string Type { get; set; } = null!; // core, elective, optional, practical

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Class Class { get; set; } = null!;
    public virtual Teacher Teacher { get; set; } = null!;
}