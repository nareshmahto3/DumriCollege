using MediatR;
using Microsoft.AspNetCore.Http;
using User.Api.DTOs;

namespace User.Api.CQRS.Command
{
    public class UploadStudentDocumentsCommand : IRequest<ResponseDto>
    {
        public string StudentId { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public IEnumerable<IFormFile> Files { get; set; } = Enumerable.Empty<IFormFile>();

        public UploadStudentDocumentsCommand(string studentId, string documentType, IEnumerable<IFormFile> files)
        {
            StudentId = studentId;
            DocumentType = documentType;
            Files = files;
        }
    }
}
