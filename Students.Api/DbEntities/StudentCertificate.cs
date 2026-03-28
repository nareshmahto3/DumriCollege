using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentCertificate
{
    public int CertificateId { get; set; }

    public int? ApplicationId { get; set; }

    public string? CertificateType { get; set; }

    public string? CertificateNumber { get; set; }

    public DateOnly? IssueDate { get; set; }

    public string? IssuedBy { get; set; }

    public string? FilePath { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual StudentApplication? Application { get; set; }

    public virtual ICollection<StudentDocumentVerification> StudentDocumentVerifications { get; set; } = new List<StudentDocumentVerification>();
}
