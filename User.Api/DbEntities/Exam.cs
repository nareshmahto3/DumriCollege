using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Exam
{
    public int Id { get; set; }

    public string ExamName { get; set; } = null!;

    public string ExamType { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string AcademicYear { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Venue { get; set; } = null!;

    public string? Instructions { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual ICollection<ExamSubject> ExamSubjects { get; set; } = new List<ExamSubject>();
}

public partial class ExamSubject
{
    public int Id { get; set; }

    public int ExamId { get; set; }

    public string Subject { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int MaxMarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Exam Exam { get; set; } = null!;
}
