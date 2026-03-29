using MediatR;
using Microsoft.AspNetCore.Http;
using Students.Api.DTOs;

namespace Students.Api.CORS.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<ResponseDto>
    {
        public StudentUpdateDto Update { get; }

        // Optional new files (agar user replace karna chahe)
        public IFormFile? CasteCertificateFile { get; }
        public IFormFile? SchoolLeavingFile { get; }
        public IFormFile? AdmitCardFile { get; }
        public IFormFile? MarksheetFile { get; }
        public IFormFile? AadhaarFile { get; }
        public IFormFile? PhotoFile { get; }

        public UpdateStudentCommand(
            StudentUpdateDto update,
            IFormFile? casteCertificateFile,
            IFormFile? schoolLeavingFile,
            IFormFile? admitCardFile,
            IFormFile? marksheetFile,
            IFormFile? aadhaarFile,
            IFormFile? photoFile)
        {
            Update = update;
            CasteCertificateFile = casteCertificateFile;
            SchoolLeavingFile = schoolLeavingFile;
            AdmitCardFile = admitCardFile;
            MarksheetFile = marksheetFile;
            AadhaarFile = aadhaarFile;
            PhotoFile = photoFile;
        }
    }
}
