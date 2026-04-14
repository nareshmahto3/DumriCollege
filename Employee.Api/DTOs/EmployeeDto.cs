namespace Employee.Api.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string? EmpId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? BloodGroup { get; set; }

        public string? City { get; set; }

        public string? Department { get; set; }

        public string? Designation { get; set; }

        public string? EmergencyContact { get; set; }

        public string? Experience { get; set; }

        public DateTime? JoiningDate { get; set; }

        public string? Qualification { get; set; }

        public string? Religion { get; set; }

        public string? ShortBio { get; set; }

        public string? State { get; set; }

        public string? Subjects { get; set; }

        public string? ZipCode { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? CreatedAt { get; set; }
    }

}

