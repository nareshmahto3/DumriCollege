using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using Students.Api.Helpers;

namespace Students.Api.CORS.RegisterStudent
{
    public class RegisterStudentCommandHandler
       : IRequestHandler<RegisterStudentCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public RegisterStudentCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<ResponseDto> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Registration;
            if (dto == null)
                return ResponseDto.Fail("Registration data is null");

            // 1) Parse dates
            DateOnly? dob = null;
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

            // 2) Subject rules based on Faculty
            var facultyRepo = _unitOfWork.Repository<Faculty>();
            var optionalRepo = _unitOfWork.Repository<OptionalSubject>();
            var compRepo = _unitOfWork.Repository<CompulsorySubject>();
            var addRepo = _unitOfWork.Repository<AdditionalSubject>();

            var faculty = await facultyRepo.GetByIdAsync(dto.FacultyId);
            if (faculty == null)
                return ResponseDto.Fail("Invalid FacultyId");

            var compulsory = await compRepo.GetByIdAsync(dto.CompulsorySubjectId);
            if (compulsory == null)
                return ResponseDto.Fail("Invalid CompulsorySubjectId");

            int maxOptional;
            switch (dto.FacultyId)
            {
                case 2: // Science
                    maxOptional = 2;
                    break;
                case 1: // Arts
                    maxOptional = 3;
                    break;
                case 3: // Commerce
                    maxOptional = 2;
                    break;
                default:
                    return ResponseDto.Fail("Unsupported FacultyId");
            }

            var optionalIds = new[]
                {
                    dto.OptionalSubject1Id,
                    dto.OptionalSubject2Id,
                    dto.OptionalSubject3Id
                }
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .ToList();

            if (optionalIds.Count > maxOptional)
                return ResponseDto.Fail($"You can select at most {maxOptional} optional subjects for this faculty.");

            if (optionalIds.Any())
            {
                var allOptionals = await optionalRepo.GetAllAsync();
                var invalid = allOptionals
                    .Where(o => optionalIds.Contains(o.OptionalSubjectId))
                    .Any(o => o.FacultyId != dto.FacultyId);

                if (invalid)
                    return ResponseDto.Fail("One or more optional subjects do not belong to the chosen faculty.");
            }

            if (dto.AdditionalSubjectId.HasValue)
            {
                var addSub = await addRepo.GetByIdAsync(dto.AdditionalSubjectId.Value);
                if (addSub == null)
                    return ResponseDto.Fail("Invalid AdditionalSubjectId");
            }

            // 3) Save files
            const long maxFileSize = 5 * 1024 * 1024;

            var casteResult = await FileStorageHelper.SaveFileAsync(
                request.CasteCertificateFile, "docs", maxFileSize, _env, cancellationToken);
            if (!casteResult.Ok) return ResponseDto.Fail(casteResult.Error!);

            var slcResult = await FileStorageHelper.SaveFileAsync(
                request.SchoolLeavingFile, "docs", maxFileSize, _env, cancellationToken);
            if (!slcResult.Ok) return ResponseDto.Fail(slcResult.Error!);

            var admitResult = await FileStorageHelper.SaveFileAsync(
                request.AdmitCardFile, "docs", maxFileSize, _env, cancellationToken);
            if (!admitResult.Ok) return ResponseDto.Fail(admitResult.Error!);

            var marksheetResult = await FileStorageHelper.SaveFileAsync(
                request.MarksheetFile, "docs", maxFileSize, _env, cancellationToken);
            if (!marksheetResult.Ok) return ResponseDto.Fail(marksheetResult.Error!);

            var aadhaarResult = await FileStorageHelper.SaveFileAsync(
                request.AadhaarFile, "docs", maxFileSize, _env, cancellationToken);
            if (!aadhaarResult.Ok) return ResponseDto.Fail(aadhaarResult.Error!);

            var photoResult = await FileStorageHelper.SaveFileAsync(
                request.PhotoFile, "photos", maxFileSize, _env, cancellationToken);
            if (!photoResult.Ok) return ResponseDto.Fail(photoResult.Error!);

            // 4) Generate unique ApplicationNo and create StudentApplication
            var appRepo = _unitOfWork.Repository<StudentApplication>();

            // ApplicationNo format: AP{AdmissionYear}{Sequence}
            // Example: AP2026001
            var admissionYear = DateTime.UtcNow.Year;

            // Load all applications once (you are already doing similar for other logic)
            var allApps = await appRepo.GetAllAsync();

            // Find max sequence for current year
            // Assumes existing ApplicationNo starts with "AP{year}" and then 3-digit sequence
            var yearPrefix = $"AP{admissionYear}";
            var yearApps = allApps
                .Where(a => a.ApplicationNo != null &&
                            a.ApplicationNo.StartsWith(yearPrefix, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int nextSequence = 1;
            if (yearApps.Any())
            {
                // Extract numeric tail and get max; be defensive against bad data
                var maxSeq = yearApps
                    .Select(a =>
                    {
                        var tail = a.ApplicationNo!.Substring(yearPrefix.Length);
                        return int.TryParse(tail, out var n) ? n : 0;
                    })
                    .Max();

                nextSequence = maxSeq + 1;
            }

            var generatedAppNo = $"{yearPrefix}{nextSequence:D3}";

            var application = new StudentApplication
            {
                StudentName = dto.StudentName,
                FatherName = dto.FatherName,
                MotherName = dto.MotherName,

                // Do NOT set RegistrationNo here
                ApplicationNo = generatedAppNo,
                ApplicationStatus = "Pending",

                PermanentAddress = dto.PermanentAddress,
                LocalAddress = dto.LocalAddress,
                DateOfBirth = dob,

                ReligionId = dto.ReligionId,
                Nationality = dto.Nationality,
                CasteId = dto.CasteId,
                BloodGroupId = dto.BloodGroupId,
                GenderId = dto.GenderId,
                CategoryId = dto.CategoryId,
                IdentificationMark = dto.IdentificationMark,
                GuardianOccupation = dto.GuardianOccupation,
                MaritalStatusId = dto.MaritalStatusId,
                AadhaarNumber = dto.AadhaarNumber,
                MobileNumber = dto.MobileNumber,
                Height = dto.Height,
                Weight = dto.Weight,
                CreatedDate = DateTime.Now
            };

            await appRepo.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();

            // 5) StudentSubjectSelection
            var subjectSelectionRepo = _unitOfWork.Repository<StudentSubjectSelection>();

            var subjectSelection = new StudentSubjectSelection
            {
                ApplicationId = application.ApplicationId,
                FacultyId = dto.FacultyId,
                CompulsorySubjectId = dto.CompulsorySubjectId,
                OptionalSubject1 = dto.OptionalSubject1Id,
                OptionalSubject2 = dto.OptionalSubject2Id,
                OptionalSubject3 = dto.OptionalSubject3Id,
                AdditionalSubjectId = dto.AdditionalSubjectId
            };

            await subjectSelectionRepo.AddAsync(subjectSelection);

            // 6) StudentExamDetail
            //var examRepo = _unitOfWork.Repository<StudentExamDetail>();

            //var examDetail = new StudentExamDetail
            //{
            //    ApplicationId = application.ApplicationId,
            //    SchoolCollege = dto.SchoolCollege,
            //    BoardCouncil = dto.BoardCouncil,
            //    ExamName = dto.ExamName,
            //    YearOfPassing = dto.YearOfPassing,
            //    DivisionOrRank = dto.DivisionOrRank,
            //    Subjects = dto.Subjects
            //};
            // 6) StudentExamDetail — save multiple exam records
            var examRepo = _unitOfWork.Repository<StudentExamDetail>();

            if (!string.IsNullOrWhiteSpace(dto.ExamDetails))
            {
                var examList = System.Text.Json.JsonSerializer.Deserialize<List<ExamDetailDto>>(
                    dto.ExamDetails,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (examList != null)
                {
                    foreach (var exam in examList)
                    {
                        var examDetail = new StudentExamDetail
                        {
                            ApplicationId = application.ApplicationId,
                            SchoolCollege = exam.SchoolCollege,
                            BoardCouncil = exam.BoardCouncil,
                            ExamName = exam.ExamName,
                            YearOfPassing = int.TryParse(exam.YearOfPassing, out var year) ? year : (int?)null,
                            DivisionOrRank = exam.DivisionOrRank,
                            Subjects = exam.Subjects
                        };

                        await examRepo.AddAsync(examDetail);
                    }
                }
            }

            // 7) StudentCertificate + StudentDocumentVerification — save and create pending verification
            var certRepo = _unitOfWork.Repository<StudentCertificate>();
            var verRepo = _unitOfWork.Repository<StudentDocumentVerification>();

            async Task SaveCertificateWithVerification(string type, string? path)
            {
                if (string.IsNullOrEmpty(path))
                    return;

                var certificate = new StudentCertificate
                {
                    ApplicationId = application.ApplicationId,
                    CertificateType = type,
                    CertificateNumber = dto.CertificateNumber,
                    IssueDate = certIssueDate,
                    IssuedBy = dto.IssuedBy,
                    FilePath = path,
                    CreatedDate = DateTime.Now
                };

                await certRepo.AddAsync(certificate);
                await _unitOfWork.SaveChangesAsync(); // ensure CertificateId is set

                var verification = new StudentDocumentVerification
                {
                    ApplicationId = application.ApplicationId,
                    CertificateId = certificate.CertificateId,
                    DocumentType = type,
                    Status = "Pending",
                    RejectReason = null,
                    IsActive = true,
                    Version = 1,
                    VerifiedDate = null,
                    CreatedDate = DateTime.Now
                };

                await verRepo.AddAsync(verification);
            }

            await SaveCertificateWithVerification("Caste Certificate", casteResult.SavedPath);
            await SaveCertificateWithVerification("School Leaving Certificate", slcResult.SavedPath);
            await SaveCertificateWithVerification("Admit Card", admitResult.SavedPath);
            await SaveCertificateWithVerification("Marksheet", marksheetResult.SavedPath);
            await SaveCertificateWithVerification("Aadhaar Card", aadhaarResult.SavedPath);
            await SaveCertificateWithVerification("Profile Photo", photoResult.SavedPath);

            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(
                new
                {
                    application.ApplicationId,
                    application.ApplicationNo,
                    application.RegistrationNo, // will be null at this stage
                    dto.StudentName,
                    Faculty = faculty.FacultyName
                },
                "Student registered successfully");
        }
    }
}