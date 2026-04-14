using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentApplication
{
    public int ApplicationId { get; set; }

    public string? StudentName { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? PermanentAddress { get; set; }

    public string? LocalAddress { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? ReligionId { get; set; }

    public string? Nationality { get; set; }

    public int? CasteId { get; set; }

    public int? BloodGroupId { get; set; }

    public int? GenderId { get; set; }

    public int? CategoryId { get; set; }

    public string? IdentificationMark { get; set; }

    public string? GuardianOccupation { get; set; }

    public int? MaritalStatusId { get; set; }

    public string? AadhaarNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? Height { get; set; }

    public string? Weight { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? RegistrationNo { get; set; }

    public string? ApplicationNo { get; set; }

    public string? ApplicationStatus { get; set; }

    public int? ClassId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual BloodGroup? BloodGroup { get; set; }

    public virtual Caste? Caste { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ClassMaster? Class { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual MaritalStatus? MaritalStatus { get; set; }

    public virtual Religion? Religion { get; set; }

    public virtual ICollection<StudentCertificate> StudentCertificates { get; set; } = new List<StudentCertificate>();

    public virtual ICollection<StudentDocumentVerification> StudentDocumentVerifications { get; set; } = new List<StudentDocumentVerification>();

    public virtual ICollection<StudentExamDetail> StudentExamDetails { get; set; } = new List<StudentExamDetail>();

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelections { get; set; } = new List<StudentSubjectSelection>();
}
