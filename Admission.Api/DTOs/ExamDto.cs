namespace Admission.Api.DTOs;

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
}
