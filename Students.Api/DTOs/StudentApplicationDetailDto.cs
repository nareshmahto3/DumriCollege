using System;
using System.Collections.Generic;

namespace Students.Api.DTOs
{
    public class StudentApplicationDetailDto
    {
        // A. Basic Info
        public int ApplicationId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string RegistrationNo { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string PermanentAddress { get; set; } = string.Empty;
        public string LocalAddress { get; set; } = string.Empty;

        // B. Lookup Names
        public string ReligionName { get; set; } = string.Empty;
        public string CasteName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;
        public string BloodGroupName { get; set; } = string.Empty;
        public string MaritalStatusName { get; set; } = string.Empty;

        // C. Subject Details (including faculty-compulsory subject)
        public string FacultyName { get; set; } = string.Empty;
        public string CompulsorySubjectName { get; set; } = string.Empty;
        public string OptionalSubject1Name { get; set; } = string.Empty;
        public string OptionalSubject2Name { get; set; } = string.Empty;
        public string OptionalSubject3Name { get; set; } = string.Empty;
        public string AdditionalSubjectName { get; set; } = string.Empty;

        // New: list of faculty compulsory subjects (from FacultyCompulsorySubject table)
        public List<string> FacultyCompulsorySubjects { get; set; } = new();

        // D. Exam Details
        public List<ExamDetailItemDto> ExamDetails { get; set; } = new();

        // E. Certificates
        public List<CertificateItemDto> Certificates { get; set; } = new();
    }

    public class ExamDetailItemDto
    {
        public string ExamName { get; set; } = string.Empty;
        public string SchoolCollege { get; set; } = string.Empty;
        public string BoardCouncil { get; set; } = string.Empty;
        public int? YearOfPassing { get; set; }
        public string DivisionOrRank { get; set; } = string.Empty;
    }

    public class CertificateItemDto
    {
        public string CertificateType { get; set; } = string.Empty;
        public string CertificateNumber { get; set; } = string.Empty;
        public DateOnly? IssueDate { get; set; }
        public string IssuedBy { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
    }
}
