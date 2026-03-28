using Microsoft.AspNetCore.Http;

namespace User.Api.DTOs
{
    public class UploadStudentDocumentDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;
    }
}
