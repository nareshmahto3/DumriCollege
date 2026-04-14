namespace Master.Api.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public int? DurationYears { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
