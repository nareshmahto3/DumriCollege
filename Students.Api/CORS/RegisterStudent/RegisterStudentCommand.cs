using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.RegisterStudent
{
    public class RegisterStudentCommand : IRequest<ResponseDto>
    {
        public StudentRegistrationDto Registration { get; }

        // Files
        public IFormFile? CasteCertificateFile { get; }
        public IFormFile? SchoolLeavingFile { get; }
        public IFormFile? AdmitCardFile { get; }
        public IFormFile? MarksheetFile { get; }
        public IFormFile? AadhaarFile { get; }
        public IFormFile? PhotoFile { get; }
        public RegisterStudentCommand(
     StudentRegistrationDto registration,
     IFormFile? casteCertificateFile,
     IFormFile? schoolLeavingFile,
     IFormFile? admitCardFile,
     IFormFile? marksheetFile,
     IFormFile? aadhaarFile,
     IFormFile? photoFile)
        {
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