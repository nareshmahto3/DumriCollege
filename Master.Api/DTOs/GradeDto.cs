namespace Master.Api.DTOs
{
    public class GradeDto
    {
        public int GradeId { get; set; }

        public string GradeName { get; set; } = null!;

        public int? MinMarks { get; set; }

        public int? MaxMarks { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
