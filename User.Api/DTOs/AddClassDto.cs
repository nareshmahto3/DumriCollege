namespace User.Api.DTOs
{
    public class AddClassDto
    {
        public string ClassName { get; set; } = null!;

        public string Section { get; set; } = null!;

        public int ClassTeacherId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int Capacity { get; set; }

        public string AcademicYear { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public string? Subjects { get; set; }
    }
}