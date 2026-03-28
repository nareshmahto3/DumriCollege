namespace Students.Api.DTOs
{
    // For admin listing
    public class DocumentVerificationDto
    {
        public int CertificateId { get; set; }
        public int ApplicationId { get; set; }

        public string? CertificateType { get; set; }
        public string? FilePath { get; set; }

        public int VerificationId { get; set; }
        public string Status { get; set; } = string.Empty; // Pending / Approved / Rejected
        public string? RejectReason { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    // Approve input
    public class ApproveDocumentDto
    {
        public int VerificationId { get; set; }
    }

    // Reject input
    public class RejectDocumentDto
    {
        public int VerificationId { get; set; }
        public string RejectReason { get; set; } = string.Empty;
    }

    // Reupload input (student)
    public class ReuploadDocumentDto
    {
        public int ApplicationId { get; set; }
        public int CertificateId { get; set; }
    }
}
