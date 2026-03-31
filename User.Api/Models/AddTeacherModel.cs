namespace User.Api.Models
{
    public class AddTeacherModel

    {
        public int Id { get; set; }

        public string? EmployeeId { get; set; } 

        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public string? Email { get; set; } 

        public string? Phone { get; set; } 

        public DateOnly? DateOfBirth { get; set; }

        public int? Gender { get; set; }

        public int? Qualification { get; set; }

        public int? Designation { get; set; }

        public int? Department { get; set; }

        public DateTime? JoiningDate { get; set; }

        public int? Experience { get; set; }

        public decimal? Salary { get; set; }

        public string? Address { get; set; }

        public int? City { get; set; }

        public int? State { get; set; }

        public int? ZipCode { get; set; }

        public int? BloodGroup { get; set; }

        public int? Religion { get; set; }

        public string? EmergencyContact { get; set; }

        public int? Subjects { get; set; }

        public string? ShortBio { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
