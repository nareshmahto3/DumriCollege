namespace User.Api.DTOs;

public class ExamDto
{
    public int Id { get; set; }
    public string ExamName { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ExamSubjectDto> ExamSubjects { get; set; } = new List<ExamSubjectDto>();
}

public class ExamSubjectDto
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxMarks { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateExamDto
{
    public string ExamName { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public List<CreateExamSubjectDto> ExamSubjects { get; set; } = new List<CreateExamSubjectDto>();
}

public class CreateExamSubjectDto
{
    public string Subject { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxMarks { get; set; }
}

public class UpdateExamDto
{
    public string ExamName { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public List<UpdateExamSubjectDto> ExamSubjects { get; set; } = new List<UpdateExamSubjectDto>();
}

public class UpdateExamSubjectDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxMarks { get; set; }
}
