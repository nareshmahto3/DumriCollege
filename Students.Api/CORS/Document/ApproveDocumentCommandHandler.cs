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
            var appRepo = _unitOfWork.Repository<StudentApplication>();

            var allVerifications = await verRepo.GetAllAsync();
            var verification = allVerifications.FirstOrDefault(v => v.Id == dto.VerificationId && v.IsActive);

            if (verification == null)
                return ResponseDto.Fail("Active verification record not found.");

            // 1) Approve this document
            verification.Status = "Approved";
            verification.RejectReason = null;
            verification.VerifiedDate = DateTime.Now;

            verRepo.Update(verification);
            await _unitOfWork.SaveChangesAsync();

            // 2) Check if all documents for this application are approved
            var applicationId = verification.ApplicationId;

            var app = await appRepo.GetByIdAsync(applicationId);
            if (app == null)
                return ResponseDto.Fail("Application not found for this document.");

            // Get all verifications for this application (only active ones matter)
            var appVerifications = allVerifications
                .Where(v => v.ApplicationId == applicationId && v.IsActive)
                .ToList();

            if (!appVerifications.Any())
            {
                // No other verifications, nothing more to do
                return ResponseDto.Success(null, "Document approved successfully.");
            }

            // If any active document is not Approved, we stop here
            var allApproved = appVerifications.All(v =>
                string.Equals(v.Status, "Approved", StringComparison.OrdinalIgnoreCase));

            if (!allApproved)
            {
                return ResponseDto.Success(null, "Document approved successfully. Waiting for remaining documents.");
            }

            // 3) All documents approved -> generate RegistrationNo if not already generated
            if (string.IsNullOrWhiteSpace(app.RegistrationNo))
            {
                // RegistrationNo format: AD/{SessionYear}/{Sequence}
                // Example: AD/2026-27/108

                var admissionYear = DateTime.UtcNow.Year;
                var nextYear = admissionYear + 1;
                var sessionYear = $"{admissionYear}-{(nextYear % 100):D2}"; // 2026-27

                var allApps = await appRepo.GetAllAsync();
                var sessionPrefix = $"AD/{sessionYear}/";

                var sessionApps = allApps
                    .Where(a => a.RegistrationNo != null &&
                                a.RegistrationNo.StartsWith(sessionPrefix, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                int nextSequence = 1;
                if (sessionApps.Any())
                {
                    var maxSeq = sessionApps
                        .Select(a =>
                        {
                            var tail = a.RegistrationNo!.Substring(sessionPrefix.Length);
                            return int.TryParse(tail, out var n) ? n : 0;
                        })
                        .Max();

                    nextSequence = maxSeq + 1;
                }

                var generatedRegNo = $"{sessionPrefix}{nextSequence}";

                app.RegistrationNo = generatedRegNo;
                app.ApplicationStatus = "Approved";

                appRepo.Update(app);
                await _unitOfWork.SaveChangesAsync();
            }

            return ResponseDto.Success(null, "Document approved successfully.");
        }
    }
}
