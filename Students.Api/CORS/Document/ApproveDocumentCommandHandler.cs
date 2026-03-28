using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class ApproveDocumentCommandHandler
        : IRequestHandler<ApproveDocumentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveDocumentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(ApproveDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var all = await verRepo.GetAllAsync();
            var verification = all.FirstOrDefault(v => v.Id == dto.VerificationId && v.IsActive);

            if (verification == null)
                return ResponseDto.Fail("Active verification record not found.");

            verification.Status = "Approved";
            verification.RejectReason = null;
            verification.VerifiedDate = DateTime.Now;

            verRepo.Update(verification);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(null, "Document approved successfully.");
        }
    }
}
