using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using Students.Api.Helpers;

namespace Students.Api.CORS.UpdateStudent
{
    public class UpdateStudentCommandHandler
        : IRequestHandler<UpdateStudentCommand, ResponseDto>
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
            var dto = request.Update;
            if (dto == null)
                return ResponseDto.Fail("Update data is null");

            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var subjectSelectionRepo = _unitOfWork.Repository<StudentSubjectSelection>();
            var examRepo = _unitOfWork.Repository<StudentExamDetail>();
            var certRepo = _unitOfWork.Repository<StudentCertificate>();

            // 1) Existing application load
            var application = await appRepo.GetByIdAsync(dto.ApplicationId);
            if (application == null)
                return ResponseDto.Fail("Application not found");

            // 2) Parse DOB (optional)
            DateOnly? dob = application.DateOfBirth;
            if (!string.IsNullOrWhiteSpace(dto.DateOfBirth))
            {
                if (!DateOnly.TryParse(dto.DateOfBirth, out var parsedDob))
                    return ResponseDto.Fail("Invalid DateOfBirth format. Use yyyy-MM-dd");
                dob = parsedDob;
            }

            // 3) Parse certificate issue date (optional, if you want)
            DateOnly? certIssueDate = null;
            if (!string.IsNullOrWhiteSpace(dto.IssueDate))
            {
                if (!DateOnly.TryParse(dto.IssueDate, out var parsedIssue))
                    return ResponseDto.Fail("Invalid Certificate IssueDate format. Use yyyy-MM-dd");
                certIssueDate = parsedIssue;
            }

            // 4) Update basic fields (only if values provided)
            if (!string.IsNullOrWhiteSpace(dto.StudentName))
                application.StudentName = dto.StudentName;

            if (!string.IsNullOrWhiteSpace(dto.FatherName))
                application.FatherName = dto.FatherName;

            if (!string.IsNullOrWhiteSpace(dto.MotherName))
                application.MotherName = dto.MotherName;

            if (!string.IsNullOrWhiteSpace(dto.PermanentAddress))
                application.PermanentAddress = dto.PermanentAddress;

            if (!string.IsNullOrWhiteSpace(dto.LocalAddress))
                application.LocalAddress = dto.LocalAddress;

            application.DateOfBirth = dob;

            if (dto.ReligionId.HasValue)
                application.ReligionId = dto.ReligionId;

            if (!string.IsNullOrWhiteSpace(dto.Nationality))
                application.Nationality = dto.Nationality;

            if (dto.CasteId.HasValue)
                application.CasteId = dto.CasteId;

            if (dto.BloodGroupId.HasValue)
                application.BloodGroupId = dto.BloodGroupId;

            if (dto.GenderId.HasValue)
                application.GenderId = dto.GenderId;

            if (dto.CategoryId.HasValue)
                application.CategoryId = dto.CategoryId;

            if (!string.IsNullOrWhiteSpace(dto.IdentificationMark))
                application.IdentificationMark = dto.IdentificationMark;

            if (!string.IsNullOrWhiteSpace(dto.GuardianOccupation))
                application.GuardianOccupation = dto.GuardianOccupation;

            if (dto.MaritalStatusId.HasValue)
                application.MaritalStatusId = dto.MaritalStatusId;

            if (!string.IsNullOrWhiteSpace(dto.AadhaarNumber))
                application.AadhaarNumber = dto.AadhaarNumber;

            if (!string.IsNullOrWhiteSpace(dto.MobileNumber))
                application.MobileNumber = dto.MobileNumber;

            if (!string.IsNullOrWhiteSpace(dto.Height))
                application.Height = dto.Height;

            if (!string.IsNullOrWhiteSpace(dto.Weight))
                application.Weight = dto.Weight;

            // 5) Subject update (optional)
            StudentSubjectSelection? subjectSelection = null;

            var existingSelectionList = await subjectSelectionRepo.GetAllAsync();
            subjectSelection = existingSelectionList
                .FirstOrDefault(x => x.ApplicationId == application.ApplicationId);

            if (subjectSelection != null)
            {
                if (dto.FacultyId.HasValue)
                    subjectSelection.FacultyId = dto.FacultyId.Value;

                if (dto.CompulsorySubjectId.HasValue)
                    subjectSelection.CompulsorySubjectId = dto.CompulsorySubjectId.Value;

                if (dto.OptionalSubject1Id.HasValue)
                    subjectSelection.OptionalSubject1 = dto.OptionalSubject1Id;

                if (dto.OptionalSubject2Id.HasValue)
                    subjectSelection.OptionalSubject2 = dto.OptionalSubject2Id;

                if (dto.OptionalSubject3Id.HasValue)
                    subjectSelection.OptionalSubject3 = dto.OptionalSubject3Id;

                if (dto.AdditionalSubjectId.HasValue)
                    subjectSelection.AdditionalSubjectId = dto.AdditionalSubjectId;
            }

            // 6) Update exam details (simple: delete old, insert new if provided)
            if (!string.IsNullOrWhiteSpace(dto.ExamDetails))
            {
                var existingExams = (await examRepo.GetAllAsync())
                    .Where(e => e.ApplicationId == application.ApplicationId)
                    .ToList();

                foreach (var ex in existingExams)
                {
                    examRepo.Remove(ex);
                }

                var examList = System.Text.Json.JsonSerializer.Deserialize<List<ExamDetailDto>>(
                    dto.ExamDetails,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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

            // 7) Files update (optional - only if new file provided)
            const long maxFileSize = 5 * 1024 * 1024;

            async Task<string?> SaveIfProvided(IFormFile? file, string folder)
            {
                if (file == null)
                    return null;

                var result = await FileStorageHelper.SaveFileAsync(file, folder, maxFileSize, _env, cancellationToken);
                if (!result.Ok)
                    throw new Exception(result.Error);

                return result.SavedPath;
            }

            string? newCastePath = null;
            string? newSlcPath = null;
            string? newAdmitPath = null;
            string? newMarksheetPath = null;
            string? newAadhaarPath = null;
            string? newPhotoPath = null;

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

            // 8) Certificate meta update + new file records, if any
            if (!string.IsNullOrWhiteSpace(dto.CertificateNumber))
            {
                // Update existing certificate meta (if you want full logic, we can expand this)
                var existingCerts = (await certRepo.GetAllAsync())
                    .Where(c => c.ApplicationId == application.ApplicationId)
                    .ToList();

                foreach (var c in existingCerts)
                {
                    c.CertificateNumber = dto.CertificateNumber;
                    c.IssueDate = certIssueDate ?? c.IssueDate;
                    if (!string.IsNullOrWhiteSpace(dto.IssuedBy))
                        c.IssuedBy = dto.IssuedBy;
                }
            }

            async Task AddCertIfNewFile(string type, string? newPath)
            {
                if (string.IsNullOrEmpty(newPath))
                    return;

                await certRepo.AddAsync(new StudentCertificate
                {
                    ApplicationId = application.ApplicationId,
                    CertificateType = type,
                    CertificateNumber = dto.CertificateNumber,
                    IssueDate = certIssueDate,
                    IssuedBy = dto.IssuedBy,
                    FilePath = newPath,
                    CreatedDate = DateTime.Now
                });
            }

            await AddCertIfNewFile("Caste Certificate", newCastePath);
            await AddCertIfNewFile("School Leaving Certificate", newSlcPath);
            await AddCertIfNewFile("Admit Card", newAdmitPath);
            await AddCertIfNewFile("Marksheet", newMarksheetPath);
            await AddCertIfNewFile("Aadhaar Card", newAadhaarPath);
            await AddCertIfNewFile("Profile Photo", newPhotoPath);

            // 9) Save all changes
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(
                new
                {
                    application.ApplicationId,
                    application.RegistrationNo,
                    application.StudentName
                },
                "Student updated successfully");
        }
    }
}