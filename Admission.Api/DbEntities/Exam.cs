using System;
using System.ComponentModel.DataAnnotations;

namespace Admission.Api.DbEntities;

public partial class Exam
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ExamName { get; set; } = string.Empty;

    [Required]
    public string ExamType { get; set; } = string.Empty;

    [Required]
    public string Class { get; set; } = string.Empty;

    [Required]
    public string AcademicYear { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Venue { get; set; } = string.Empty;

    public string? Instructions { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
