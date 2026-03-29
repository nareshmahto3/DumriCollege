namespace Students.Api.DTOs
{
    public class StudentUpdateDto
    {
        // Jis application ko update karna hai
        public int ApplicationId { get; set; }

        // Updatable fields (jo tum allow karna chaho)
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }

        public string? PermanentAddress { get; set; }
        public string? LocalAddress { get; set; }

        // "yyyy-MM-dd"
        public string? DateOfBirth { get; set; }

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

        // Subjects
        public int? FacultyId { get; set; }
        public int? CompulsorySubjectId { get; set; }

        public int? OptionalSubject1Id { get; set; }
        public int? OptionalSubject2Id { get; set; }
        public int? OptionalSubject3Id { get; set; }
        public int? AdditionalSubjectId { get; set; }

        // Optional: exam update (JSON), same format as create
        public string? ExamDetails { get; set; }

        // Certificate meta update (agar chahiye)
        public string? CertificateNumber { get; set; }
        public string? IssueDate { get; set; } // "yyyy-MM-dd"
        public string? IssuedBy { get; set; }
    }
}
