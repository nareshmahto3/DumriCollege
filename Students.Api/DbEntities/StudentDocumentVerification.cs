using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentDocumentVerification
{
    public int Id { get; set; }

    public int CertificateId { get; set; }

    public int ApplicationId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? RejectReason { get; set; }

    public bool IsActive { get; set; }

    public int Version { get; set; }

    public DateTime? VerifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual StudentApplication Application { get; set; } = null!;

    public virtual StudentCertificate Certificate { get; set; } = null!;
}
