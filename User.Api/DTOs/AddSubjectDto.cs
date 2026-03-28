namespace User.Api.DTOs
{
    public class AddSubjectDto
    {
        public string SubjectName { get; set; } = null!;

        public string SubjectCode { get; set; } = null!;

        public int ClassId { get; set; }

        public int TeacherId { get; set; }

        public int Credits { get; set; }

        public string Type { get; set; } = null!;

        public string? Description { get; set; }
    }
}