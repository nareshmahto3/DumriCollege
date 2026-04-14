using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<ResponseDto>
    {
        public int ApplicationId { get; }
        public StudentRegistrationDto Registration { get; }

        public IFormFile? CasteCertificateFile { get; }
        public IFormFile? SchoolLeavingFile { get; }
        public IFormFile? AdmitCardFile { get; }
        public IFormFile? MarksheetFile { get; }
        public IFormFile? AadhaarFile { get; }
        public IFormFile? PhotoFile { get; }

        public UpdateStudentCommand(
            int applicationId,
            StudentRegistrationDto registration,
            IFormFile? casteCertificateFile,
            IFormFile? schoolLeavingFile,
            IFormFile? admitCardFile,
            IFormFile? marksheetFile,
            IFormFile? aadhaarFile,
            IFormFile? photoFile)
        {
            ApplicationId = applicationId;
            Registration = registration;
            CasteCertificateFile = casteCertificateFile;
            SchoolLeavingFile = schoolLeavingFile;
            AdmitCardFile = admitCardFile;
            MarksheetFile = marksheetFile;
            AadhaarFile = aadhaarFile;
            PhotoFile = photoFile;
        }
    }
}
