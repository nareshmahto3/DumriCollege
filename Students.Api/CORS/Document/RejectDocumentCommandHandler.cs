using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class RejectDocumentCommandHandler
        : IRequestHandler<RejectDocumentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectDocumentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(RejectDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.RejectReason))
                return ResponseDto.Fail("RejectReason is required when rejecting a document.");

            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var all = await verRepo.GetAllAsync();
            var verification = all.FirstOrDefault(v => v.Id == dto.VerificationId && v.IsActive);

            if (verification == null)
                return ResponseDto.Fail("Active verification record not found.");

            verification.Status = "Rejected";
            verification.RejectReason = dto.RejectReason;
            verification.VerifiedDate = DateTime.Now;

            verRepo.Update(verification);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(null, "Document rejected successfully.");
        }
    }
}
