using System.Globalization;
using System.Linq;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using User.Api.CQRS.Command;
using User.Api.DbEntities;
using User.Api.DTOs;

namespace User.Api.CQRS.CommandHandler;

public class UploadStudentDocumentsCommandHandler : IRequestHandler<UploadStudentDocumentsCommand, ResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UploadStudentDocumentsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> Handle(UploadStudentDocumentsCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.StudentId))
            return ResponseDto.Fail("StudentId is required");

        if (request.Files == null || !request.Files.Any())
            return ResponseDto.Fail("At least one file must be uploaded");

        var repository = _unitOfWork.Repository<StudentDocument>();
        var storedDocuments = new List<StudentDocument>();

        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };

        foreach (var file in request.Files)
        {
            if (file.Length == 0)
                continue;

            if (file.Length > 5 * 1024 * 1024)
                return ResponseDto.Fail($"File {file.FileName} exceeds maximum size 5MB");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return ResponseDto.Fail($"File {file.FileName} type is not allowed. Allowed: PDF, JPG, JPEG, PNG");

            string safeFileName = Path.GetFileNameWithoutExtension(file.FileName);
            safeFileName = string.Concat(safeFileName.Split(Path.GetInvalidFileNameChars())).Replace(' ', '_');
            var uniqueFileName = $"{Guid.NewGuid():N}_{safeFileName}{extension}";
            var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "student-documents", request.StudentId, request.DocumentType.Replace(" ", "-").ToLowerInvariant());

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            var savedPath = Path.Combine(targetDirectory, uniqueFileName);
            await using (var fileStream = new FileStream(savedPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            var document = new StudentDocument
            {
                StudentId = request.StudentId,
                DocumentType = request.DocumentType,
                DocumentName = request.DocumentType,
                OriginalFileName = file.FileName,
                FilePath = Path.Combine("uploads", "student-documents", request.StudentId, request.DocumentType.Replace(" ", "-").ToLowerInvariant(), uniqueFileName).Replace("\\", "/"),
                FileSize = file.Length,
                Status = "pending",
                UploadDate = DateTime.UtcNow,
                Remarks = null
            };

            await repository.AddAsync(document);
            storedDocuments.Add(document);
        }

        await _unitOfWork.SaveChangesAsync();

        return ResponseDto.Success(storedDocuments.Select(x => new StudentDocumentDto
        {
            Id = x.Id,
            StudentId = x.StudentId,
            DocumentName = x.DocumentName,
            DocumentType = x.DocumentType,
            OriginalFileName = x.OriginalFileName,
            FilePath = x.FilePath,
            FileSize = x.FileSize,
            Status = x.Status,
            UploadDate = x.UploadDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            VerifiedDate = x.VerifiedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            Remarks = x.Remarks
        }), "Documents uploaded successfully");
    }
}

public class GetAllCertificatesQueryHandler : IRequestHandler<GetAllCertificatesQuery, ResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCertificatesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> Handle(GetAllCertificatesQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<StudentDocument>();
        var allDocs = await repository.GetAllAsync();

        var response = allDocs.Select(doc => new StudentDocumentDto
        {
            Id = doc.Id,
            StudentId = doc.StudentId,
            DocumentName = doc.DocumentName,
            DocumentType = doc.DocumentType,
            OriginalFileName = doc.OriginalFileName,
            FilePath = doc.FilePath,
            FileSize = doc.FileSize,
            Status = doc.Status,
            UploadDate = doc.UploadDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            VerifiedDate = doc.VerifiedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            Remarks = doc.Remarks
        }).ToList();

        return ResponseDto.Success(response, "Certificates fetched successfully");
    }
}

public class UpdateCertificateStatusCommandHandler : IRequestHandler<UpdateCertificateStatusCommand, ResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCertificateStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private static readonly string[] AllowedStatuses = new[] { "pending", "verified", "rejected" };

    public async Task<ResponseDto> Handle(UpdateCertificateStatusCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<StudentDocument>();
        var doc = await repository.GetByIdAsync(request.DocumentId);

        if (doc == null)
            return ResponseDto.Fail("Certificate not found");

        var newStatus = request.Status.Trim().ToLowerInvariant();
        if (!AllowedStatuses.Contains(newStatus))
            return ResponseDto.Fail("Status must be 'pending', 'verified', or 'rejected'");

        doc.Status = newStatus;
        doc.Remarks = request.Remarks;
        doc.VerifiedDate = newStatus == "pending" ? null : DateTime.UtcNow;

        repository.Update(doc);
        await _unitOfWork.SaveChangesAsync();

        return ResponseDto.Success(new StudentDocumentDto
        {
            Id = doc.Id,
            StudentId = doc.StudentId,
            DocumentName = doc.DocumentName,
            DocumentType = doc.DocumentType,
            OriginalFileName = doc.OriginalFileName,
            FilePath = doc.FilePath,
            FileSize = doc.FileSize,
            Status = doc.Status,
            UploadDate = doc.UploadDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            VerifiedDate = doc.VerifiedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
            Remarks = doc.Remarks
        }, "Certificate status updated");
    }
}
