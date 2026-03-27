using System;

namespace User.Api.DbEntities;

public partial class StudentDocument
{
    public int Id { get; set; }
    public string StudentId { get; set; } = null!;
    public string DocumentName { get; set; } = null!;
    public string DocumentType { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public long FileSize { get; set; }
    public string Status { get; set; } = "pending";
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    public DateTime? VerifiedDate { get; set; }
    public string? Remarks { get; set; }
}
