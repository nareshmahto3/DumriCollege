using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using Students.Api.Helpers;

namespace Students.Api.CORS.Document
{
    public class ReuploadDocumentCommandHandler
        : IRequestHandler<ReuploadDocumentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ReuploadDocumentCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<ResponseDto> Handle(ReuploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Reupload;

            if (request.File == null || request.File.Length == 0)
                return ResponseDto.Fail("File is required for re-upload.");

            var certRepo = _unitOfWork.Repository<StudentCertificate>();
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var certs = await certRepo.GetAllAsync();
            var cert = certs.FirstOrDefault(c =>
                c.ApplicationId == dto.ApplicationId &&
                c.CertificateId == dto.CertificateId);

            if (cert == null)
                return ResponseDto.Fail("Certificate not found for this application.");

            var verifications = await verRepo.GetAllAsync();
            var activeVerification = verifications
                .Where(v => v.CertificateId == cert.CertificateId && v.IsActive)
                .OrderByDescending(v => v.Version)
                .FirstOrDefault();

            if (activeVerification == null)
                return ResponseDto.Fail("No active verification record found for this document.");

            if (!string.Equals(activeVerification.Status, "Rejected", StringComparison.OrdinalIgnoreCase))
                return ResponseDto.Fail("Re-upload is only allowed when document status is Rejected.");

            // Save new file
            const long maxFileSize = 5 * 1024 * 1024;
            var saveResult = await FileStorageHelper.SaveFileAsync(
                request.File,
                "docs",
                maxFileSize,
                _env,
                cancellationToken);

            if (!saveResult.Ok)
                return ResponseDto.Fail(saveResult.Error ?? "File save failed.");

            // Insert new certificate record
            var newCert = new StudentCertificate
            {
                ApplicationId = cert.ApplicationId,
                CertificateType = cert.CertificateType,
                CertificateNumber = cert.CertificateNumber,
                IssueDate = cert.IssueDate,
                IssuedBy = cert.IssuedBy,
                FilePath = saveResult.SavedPath,
                CreatedDate = DateTime.Now
            };

            await certRepo.AddAsync(newCert);
            await _unitOfWork.SaveChangesAsync(); // get new CertificateId

            // Deactivate old verification
            activeVerification.IsActive = false;
            verRepo.Update(activeVerification);

            // New verification record
            var newVersion = activeVerification.Version + 1;

            var newVerification = new StudentDocumentVerification
            {
                ApplicationId = cert.ApplicationId ?? 0,
                CertificateId = newCert.CertificateId,
                DocumentType = activeVerification.DocumentType,
                Status = "Pending",
                RejectReason = null,
                VerifiedDate = null,
                IsActive = true,
                Version = newVersion,
                CreatedDate = DateTime.Now
            };

            await verRepo.AddAsync(newVerification);

            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(null, "Document re-uploaded and set to Pending for verification.");
        }
    }
}
