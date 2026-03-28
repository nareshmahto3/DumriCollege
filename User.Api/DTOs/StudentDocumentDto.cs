namespace User.Api.DTOs
{
    public class StudentDocumentDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string Status { get; set; } = string.Empty;
        public string UploadDate { get; set; } = string.Empty;
        public string? VerifiedDate { get; set; }
        public string? Remarks { get; set; }
    }
}
