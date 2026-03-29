namespace Students.Api.DTOs
{
    public class StudentRegistrationDto
    {
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? RegistrationNo { get; set; }


        public string? PermanentAddress { get; set; }
        public string? LocalAddress { get; set; }

        public string? DateOfBirth { get; set; } // "yyyy-MM-dd"

        //public string? Religion { get; set; }
        public int? ReligionId { get; set; }
        public string? Nationality { get; set; }
    public int? CasteId { get; set; }

        //public string? BloodGroup { get; set; }
        public int? BloodGroupId { get; set; }
        //public string? Gender { get; set; }
        //public string? Category { get; set; }
        public int? GenderId { get; set; }
        public int? CategoryId { get; set; }

        public string? IdentificationMark { get; set; }
        public string? GuardianOccupation { get; set; }
        //public string? MaritalStatus { get; set; }
        public int? MaritalStatusId { get; set; }
        public string? AadhaarNumber { get; set; }

        public string? MobileNumber { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }

        // Subject selection
        public int FacultyId { get; set; }                // FK to Faculty
        public int CompulsorySubjectId { get; set; }      // FK to CompulsorySubjects table

        // Optional subjects – IDs from OptionalSubjects table
        public int? OptionalSubject1Id { get; set; }
        public int? OptionalSubject2Id { get; set; }
        public int? OptionalSubject3Id { get; set; }

        // Additional subject – ID from AdditionalSubjects table
        public int? AdditionalSubjectId { get; set; }

        // Exam detail
        //public string? SchoolCollege { get; set; }
        //public string? BoardCouncil { get; set; }
        //public string? ExamName { get; set; }
        //public int? YearOfPassing { get; set; }
        //public string? DivisionOrRank { get; set; }
        //public string? Subjects { get; set; }
        public string? ExamDetails { get; set; }

        // Certificate info (meta, not file paths)
        public string? CertificateType { get; set; }
        public string? CertificateNumber { get; set; }
        public string? IssueDate { get; set; } // "yyyy-MM-dd"
        public string? IssuedBy { get; set; }
    }
}
