using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class GetDocumentsByApplicationIdQueryHandler
        : IRequestHandler<GetDocumentsByApplicationIdQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDocumentsByApplicationIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetDocumentsByApplicationIdQuery request, CancellationToken cancellationToken)
        {
            var certRepo = _unitOfWork.Repository<StudentCertificate>();
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var certificates = await certRepo.GetAllAsync();
            var verifications = await verRepo.GetAllAsync();

            var certList = certificates
                .Where(c => c.ApplicationId == request.ApplicationId)
                .ToList();

            if (!certList.Any())
                return ResponseDto.Fail("No documents found for this application.");

            var result = certList
                .Select(cert =>
                {
                    var latestActive = verifications
                        .Where(v => v.CertificateId == cert.CertificateId && v.IsActive)
                        .OrderByDescending(v => v.Version)
                        .FirstOrDefault();

                    return new DocumentVerificationDto
                    {
                        CertificateId = cert.CertificateId,
                        ApplicationId = cert.ApplicationId ?? 0,
                        CertificateType = cert.CertificateType,
                        FilePath = cert.FilePath,
                        VerificationId = latestActive?.Id ?? 0,
                        Status = latestActive?.Status ?? "Pending",
                        RejectReason = latestActive?.RejectReason,
                        VerifiedDate = latestActive?.VerifiedDate,
                        IsActive = latestActive?.IsActive ?? false,
                        Version = latestActive?.Version ?? 0,
                        CreatedDate = latestActive?.CreatedDate ?? cert.CreatedDate ?? DateTime.MinValue
                    };
                })
                .ToList();

            return ResponseDto.Success(result, "Documents fetched successfully.");
        }
    }
}
