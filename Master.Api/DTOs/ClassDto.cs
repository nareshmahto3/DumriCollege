namespace Master.Api.DTOs
{
    public class ClassDto
    {
        public int ClassId { get; set; }

        public int? CourseId { get; set; }
        public string CourseName { get; set; } = null!;

        public string ClassName { get; set; } = null!;

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
