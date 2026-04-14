using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using Students.Api.Helpers;

namespace Students.Api.CORS.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public UpdateStudentCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<ResponseDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.ApplicationId <= 0)
                return ResponseDto.Fail("ApplicationId is required.");

            var dto = request.Registration;
            if (dto == null)
                return ResponseDto.Fail("Registration data is null");

            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var subjectSelectionRepo = _unitOfWork.Repository<StudentSubjectSelection>();
            var examRepo = _unitOfWork.Repository<StudentExamDetail>();
            var certRepo = _unitOfWork.Repository<StudentCertificate>();
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            var facultyRepo = _unitOfWork.Repository<Faculty>();
            var optionalRepo = _unitOfWork.Repository<OptionalSubject>();
            var compRepo = _unitOfWork.Repository<CompulsorySubject>();
            var addRepo = _unitOfWork.Repository<AdditionalSubject>();
            var classRepo = _unitOfWork.Repository<ClassMaster>();

            var application = await appRepo.GetByIdAsync(request.ApplicationId);
            if (application == null || !application.IsActive)
                return ResponseDto.Fail("Application not found");

            DateOnly? dob = application.DateOfBirth;
            if (!string.IsNullOrWhiteSpace(dto.DateOfBirth))
            {
                if (!DateOnly.TryParse(dto.DateOfBirth, out var parsedDob))
                    return ResponseDto.Fail("Invalid DateOfBirth format. Use yyyy-MM-dd");
                dob = parsedDob;
            }

            DateOnly? certIssueDate = null;
            if (!string.IsNullOrWhiteSpace(dto.IssueDate))
            {
                if (!DateOnly.TryParse(dto.IssueDate, out var parsedIssue))
                    return ResponseDto.Fail("Invalid Certificate IssueDate format. Use yyyy-MM-dd");
                certIssueDate = parsedIssue;
            }

            var existingSelection = (await subjectSelectionRepo.GetAllAsync())
                .FirstOrDefault(x => x.ApplicationId == application.ApplicationId && x.IsActive);

            var targetFacultyId = dto.FacultyId > 0 ? dto.FacultyId : (existingSelection?.FacultyId ?? 0);
            var targetCompulsorySubjectId = dto.CompulsorySubjectId > 0 ? dto.CompulsorySubjectId : (existingSelection?.CompulsorySubjectId ?? 0);

            if (targetFacultyId > 0)
            {
                var faculty = await facultyRepo.GetByIdAsync(targetFacultyId);
                if (faculty == null)
                    return ResponseDto.Fail("Invalid FacultyId");
            }

            if (targetCompulsorySubjectId > 0)
            {
                var compulsory = await compRepo.GetByIdAsync(targetCompulsorySubjectId);
                if (compulsory == null)
                    return ResponseDto.Fail("Invalid CompulsorySubjectId");
            }

            if (dto.ClassId.HasValue)
            {
                var classMaster = await classRepo.GetByIdAsync(dto.ClassId.Value);
                if (classMaster == null)
                    return ResponseDto.Fail("Invalid ClassId");
            }

            var optionalIds = new[] { dto.OptionalSubject1Id, dto.OptionalSubject2Id, dto.OptionalSubject3Id }
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .ToList();

            if (targetFacultyId > 0)
            {
                int maxOptional;
                switch (targetFacultyId)
                {
                    case 2:
                        maxOptional = 2;
                        break;
                    case 1:
                        maxOptional = 3;
                        break;
                    case 3:
                        maxOptional = 2;
                        break;
                    default:
                        return ResponseDto.Fail("Unsupported FacultyId");
                }

                if (optionalIds.Count > maxOptional)
                    return ResponseDto.Fail($"You can select at most {maxOptional} optional subjects for this faculty.");

                if (optionalIds.Any())
                {
                    var allOptionals = await optionalRepo.GetAllAsync();
                    var invalid = allOptionals
                        .Where(o => optionalIds.Contains(o.OptionalSubjectId))
                        .Any(o => o.FacultyId != targetFacultyId);

                    if (invalid)
                        return ResponseDto.Fail("One or more optional subjects do not belong to the chosen faculty.");
                }
            }

            if (dto.AdditionalSubjectId.HasValue)
            {
                var addSub = await addRepo.GetByIdAsync(dto.AdditionalSubjectId.Value);
                if (addSub == null)
                    return ResponseDto.Fail("Invalid AdditionalSubjectId");
            }

            if (!string.IsNullOrWhiteSpace(dto.StudentName)) application.StudentName = dto.StudentName;
            if (!string.IsNullOrWhiteSpace(dto.FatherName)) application.FatherName = dto.FatherName;
            if (!string.IsNullOrWhiteSpace(dto.MotherName)) application.MotherName = dto.MotherName;
            if (!string.IsNullOrWhiteSpace(dto.PermanentAddress)) application.PermanentAddress = dto.PermanentAddress;
            if (!string.IsNullOrWhiteSpace(dto.LocalAddress)) application.LocalAddress = dto.LocalAddress;

            application.DateOfBirth = dob;

            if (dto.ReligionId.HasValue) application.ReligionId = dto.ReligionId;
            if (!string.IsNullOrWhiteSpace(dto.Nationality)) application.Nationality = dto.Nationality;
            if (dto.CasteId.HasValue) application.CasteId = dto.CasteId;
            if (dto.BloodGroupId.HasValue) application.BloodGroupId = dto.BloodGroupId;
            if (dto.GenderId.HasValue) application.GenderId = dto.GenderId;
            if (dto.CategoryId.HasValue) application.CategoryId = dto.CategoryId;
            if (dto.ClassId.HasValue) application.ClassId = dto.ClassId;
            if (!string.IsNullOrWhiteSpace(dto.IdentificationMark)) application.IdentificationMark = dto.IdentificationMark;
            if (!string.IsNullOrWhiteSpace(dto.GuardianOccupation)) application.GuardianOccupation = dto.GuardianOccupation;
            if (dto.MaritalStatusId.HasValue) application.MaritalStatusId = dto.MaritalStatusId;
            if (!string.IsNullOrWhiteSpace(dto.AadhaarNumber)) application.AadhaarNumber = dto.AadhaarNumber;
            if (!string.IsNullOrWhiteSpace(dto.MobileNumber)) application.MobileNumber = dto.MobileNumber;
            if (!string.IsNullOrWhiteSpace(dto.Height)) application.Height = dto.Height;
            if (!string.IsNullOrWhiteSpace(dto.Weight)) application.Weight = dto.Weight;

            application.ModifiedDate = DateTime.Now;
            application.ModifiedBy = 0;
            application.IsActive = true;

            if (existingSelection == null)
            {
                if (targetFacultyId > 0 && targetCompulsorySubjectId > 0)
                {
                    await subjectSelectionRepo.AddAsync(new StudentSubjectSelection
                    {
                        ApplicationId = application.ApplicationId,
                        FacultyId = targetFacultyId,
                        CompulsorySubjectId = targetCompulsorySubjectId,
                        OptionalSubject1 = dto.OptionalSubject1Id,
                        OptionalSubject2 = dto.OptionalSubject2Id,
                        OptionalSubject3 = dto.OptionalSubject3Id,
                        AdditionalSubjectId = dto.AdditionalSubjectId,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 0,
                        IsActive = true
                    });
                }
            }
            else
            {
                if (dto.FacultyId > 0) existingSelection.FacultyId = dto.FacultyId;
                if (dto.CompulsorySubjectId > 0) existingSelection.CompulsorySubjectId = dto.CompulsorySubjectId;
                if (dto.OptionalSubject1Id.HasValue) existingSelection.OptionalSubject1 = dto.OptionalSubject1Id;
                if (dto.OptionalSubject2Id.HasValue) existingSelection.OptionalSubject2 = dto.OptionalSubject2Id;
                if (dto.OptionalSubject3Id.HasValue) existingSelection.OptionalSubject3 = dto.OptionalSubject3Id;
                if (dto.AdditionalSubjectId.HasValue) existingSelection.AdditionalSubjectId = dto.AdditionalSubjectId;

                existingSelection.ModifiedDate = DateTime.Now;
                existingSelection.ModifiedBy = 0;
                existingSelection.IsActive = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.ExamDetails))
            {
                var existingExams = (await examRepo.GetAllAsync())
                    .Where(e => e.ApplicationId == application.ApplicationId && e.IsActive)
                    .ToList();

                foreach (var ex in existingExams)
                {
                    ex.IsActive = false;
                    ex.ModifiedDate = DateTime.Now;
                    ex.ModifiedBy = 0;
                }

                var examList = System.Text.Json.JsonSerializer.Deserialize<List<ExamDetailDto>>(
                    dto.ExamDetails,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (examList != null)
                {
                    foreach (var exam in examList)
                    {
                        await examRepo.AddAsync(new StudentExamDetail
                        {
                            ApplicationId = application.ApplicationId,
                            SchoolCollege = exam.SchoolCollege,
                            BoardCouncil = exam.BoardCouncil,
                            ExamName = exam.ExamName,
                            YearOfPassing = int.TryParse(exam.YearOfPassing, out var year) ? year : (int?)null,
                            DivisionOrRank = exam.DivisionOrRank,
                            Subjects = exam.Subjects,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 0,
                            IsActive = true
                        });
                    }
                }
            }

            const long maxFileSize = 5 * 1024 * 1024;
            async Task<string?> SaveIfProvided(IFormFile? file, string folder)
            {
                if (file == null) return null;

                var result = await FileStorageHelper.SaveFileAsync(file, folder, maxFileSize, _env, cancellationToken);
                if (!result.Ok) throw new Exception(result.Error);

                return result.SavedPath;
            }

            string? newCastePath;
            string? newSlcPath;
            string? newAdmitPath;
            string? newMarksheetPath;
            string? newAadhaarPath;
            string? newPhotoPath;

            try
            {
                newCastePath = await SaveIfProvided(request.CasteCertificateFile, "docs");
                newSlcPath = await SaveIfProvided(request.SchoolLeavingFile, "docs");
                newAdmitPath = await SaveIfProvided(request.AdmitCardFile, "docs");
                newMarksheetPath = await SaveIfProvided(request.MarksheetFile, "docs");
                newAadhaarPath = await SaveIfProvided(request.AadhaarFile, "docs");
                newPhotoPath = await SaveIfProvided(request.PhotoFile, "photos");
            }
            catch (Exception ex)
            {
                return ResponseDto.Fail(ex.Message);
            }

            var existingCerts = (await certRepo.GetAllAsync())
                .Where(c => c.ApplicationId == application.ApplicationId && c.IsActive)
                .ToList();

            var existingVerifications = (await verRepo.GetAllAsync())
                .Where(v => v.ApplicationId == application.ApplicationId && v.IsActive)
                .ToList();

            async Task UpsertCertificateAndVerification(string type, string? newPath)
            {
                var cert = existingCerts.FirstOrDefault(c => string.Equals(c.CertificateType, type, StringComparison.OrdinalIgnoreCase));

                if (cert == null)
                {
                    if (string.IsNullOrWhiteSpace(newPath))
                        return;

                    cert = new StudentCertificate
                    {
                        ApplicationId = application.ApplicationId,
                        CertificateType = type,
                        CertificateNumber = dto.CertificateNumber,
                        IssueDate = certIssueDate,
                        IssuedBy = dto.IssuedBy,
                        FilePath = newPath,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 0,
                        IsActive = true
                    };

                    await certRepo.AddAsync(cert);
                    await _unitOfWork.SaveChangesAsync();

                    await verRepo.AddAsync(new StudentDocumentVerification
                    {
                        ApplicationId = application.ApplicationId,
                        CertificateId = cert.CertificateId,
                        DocumentType = type,
                        Status = "Pending",
                        RejectReason = null,
                        IsActive = true,
                        Version = 1,
                        VerifiedDate = null,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 0
                    });

                    return;
                }

                if (!string.IsNullOrWhiteSpace(dto.CertificateNumber)) cert.CertificateNumber = dto.CertificateNumber;
                if (certIssueDate.HasValue) cert.IssueDate = certIssueDate;
                if (!string.IsNullOrWhiteSpace(dto.IssuedBy)) cert.IssuedBy = dto.IssuedBy;

                var uploadedNewFile = !string.IsNullOrWhiteSpace(newPath);
                if (uploadedNewFile) cert.FilePath = newPath;

                cert.ModifiedDate = DateTime.Now;
                cert.ModifiedBy = 0;
                cert.IsActive = true;

                var verification = existingVerifications.FirstOrDefault(v => v.CertificateId == cert.CertificateId);
                if (verification == null)
                {
                    await verRepo.AddAsync(new StudentDocumentVerification
                    {
                        ApplicationId = application.ApplicationId,
                        CertificateId = cert.CertificateId,
                        DocumentType = type,
                        Status = "Pending",
                        RejectReason = null,
                        IsActive = true,
                        Version = 1,
                        VerifiedDate = null,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 0
                    });
                }
                else
                {
                    verification.DocumentType = type;
                    verification.IsActive = true;
                    verification.ModifiedDate = DateTime.Now;
                    verification.ModifiedBy = 0;

                    if (uploadedNewFile)
                    {
                        verification.Status = "Pending";
                        verification.RejectReason = null;
                        verification.VerifiedDate = null;
                        verification.Version = verification.Version + 1;
                    }
                }
            }

            await UpsertCertificateAndVerification("Caste Certificate", newCastePath);
            await UpsertCertificateAndVerification("School Leaving Certificate", newSlcPath);
            await UpsertCertificateAndVerification("Admit Card", newAdmitPath);
            await UpsertCertificateAndVerification("Marksheet", newMarksheetPath);
            await UpsertCertificateAndVerification("Aadhaar Card", newAadhaarPath);
            await UpsertCertificateAndVerification("Profile Photo", newPhotoPath);

            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(
                new
                {
                    application.ApplicationId,
                    application.ApplicationNo,
                    application.RegistrationNo,
                    application.StudentName
                },
                "Student updated successfully");
        }
    }
}
