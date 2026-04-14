namespace Students.Api.DTOs
{
    public class StudentListDto
    {
        public int ApplicationId { get; set; }
        public string ApplicationNo { get; set; } = string.Empty;
        public string RegistrationNo { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string ApplicationStatus { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;
        public string? StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
    }
}
