using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.ApplicationId <= 0)
                return ResponseDto.Fail("ApplicationId is required.");

            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var subjectRepo = _unitOfWork.Repository<StudentSubjectSelection>();
            var examRepo = _unitOfWork.Repository<StudentExamDetail>();
            var certRepo = _unitOfWork.Repository<StudentCertificate>();
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var application = await appRepo.GetByIdAsync(request.ApplicationId);
            if (application == null)
                return ResponseDto.Fail("Application not found");

            if (!application.IsActive)
                return ResponseDto.Success(null, "Student application already deleted");

            var now = DateTime.Now;

            application.IsActive = false;
            application.ApplicationStatus = "Deleted";
            application.ModifiedDate = now;
            application.ModifiedBy = 0;

            var subjectRows = (await subjectRepo.GetAllAsync())
                .Where(x => x.ApplicationId == request.ApplicationId && x.IsActive)
                .ToList();
            foreach (var row in subjectRows)
            {
                row.IsActive = false;
                row.ModifiedDate = now;
                row.ModifiedBy = 0;
            }

            var examRows = (await examRepo.GetAllAsync())
                .Where(x => x.ApplicationId == request.ApplicationId && x.IsActive)
                .ToList();
            foreach (var row in examRows)
            {
                row.IsActive = false;
                row.ModifiedDate = now;
                row.ModifiedBy = 0;
            }

            var certRows = (await certRepo.GetAllAsync())
                .Where(x => x.ApplicationId == request.ApplicationId && x.IsActive)
                .ToList();
            foreach (var row in certRows)
            {
                row.IsActive = false;
                row.ModifiedDate = now;
                row.ModifiedBy = 0;
            }

            var verificationRows = (await verRepo.GetAllAsync())
                .Where(x => x.ApplicationId == request.ApplicationId && x.IsActive)
                .ToList();
            foreach (var row in verificationRows)
            {
                row.IsActive = false;
                row.ModifiedDate = now;
                row.ModifiedBy = 0;
            }

            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(null, "Student application deleted successfully");
        }
    }
}
