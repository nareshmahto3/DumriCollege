using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class StudentApplication
{
    public int ApplicationId { get; set; }

    public string? StudentName { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? PermanentAddress { get; set; }

    public string? LocalAddress { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public string? Caste { get; set; }

    public string? BloodGroup { get; set; }

    public string? Gender { get; set; }

    public string? Category { get; set; }

    public string? IdentificationMark { get; set; }

    public string? GuardianOccupation { get; set; }

    public string? MaritalStatus { get; set; }

    public string? AadhaarNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? Height { get; set; }

    public string? Weight { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<StudentCertificate> StudentCertificates { get; set; } = new List<StudentCertificate>();

    public virtual ICollection<StudentExamDetail> StudentExamDetails { get; set; } = new List<StudentExamDetail>();

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelections { get; set; } = new List<StudentSubjectSelection>();
}
