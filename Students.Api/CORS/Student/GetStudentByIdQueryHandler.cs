using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Students.Api.DbConnection;
using Students.Api.DTOs;

namespace Students.Api.CORS.Student
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, ResponseDto>
    {
        private readonly StudentDbContext _dbContext;

        public GetStudentByIdQueryHandler(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            // 1) Load base application + all needed navigation in a single query
            var query = _dbContext.StudentApplications
                .AsNoTracking()
                .Where(a => a.ApplicationId == request.ApplicationId)
                .Where(a => a.IsActive)
                .Include(a => a.Religion)
                .Include(a => a.Caste)
                .Include(a => a.Category)
                .Include(a => a.Class)
                .Include(a => a.Gender)
                .Include(a => a.BloodGroup)
                .Include(a => a.MaritalStatus)
                .Include(a => a.StudentExamDetails)
                .Include(a => a.StudentCertificates)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.Faculty)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.CompulsorySubject)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.OptionalSubject1Navigation)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.OptionalSubject2Navigation)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.OptionalSubject3Navigation)
                .Include(a => a.StudentSubjectSelections)
                    .ThenInclude(s => s.AdditionalSubject);

            var entity = await query.FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
                return ResponseDto.Fail("Student application not found.");

            // 2) Load FacultyCompulsorySubject list for the student's faculty (if any)
            string facultyName = string.Empty;
            string compName = string.Empty;
            string opt1Name = string.Empty;
            string opt2Name = string.Empty;
            string opt3Name = string.Empty;
            string addName = string.Empty;
            var facultyCompulsorySubjects = new System.Collections.Generic.List<string>();

            var firstSelection = entity.StudentSubjectSelections
                .Where(s => s.IsActive)
                .OrderBy(s => s.Id)
                .FirstOrDefault();

            if (firstSelection != null)
            {
                facultyName = firstSelection.Faculty?.FacultyName ?? string.Empty;
                compName = firstSelection.CompulsorySubject?.SubjectName ?? string.Empty;
                opt1Name = firstSelection.OptionalSubject1Navigation?.SubjectName ?? string.Empty;
                opt2Name = firstSelection.OptionalSubject2Navigation?.SubjectName ?? string.Empty;
                opt3Name = firstSelection.OptionalSubject3Navigation?.SubjectName ?? string.Empty;
                addName = firstSelection.AdditionalSubject?.SubjectName ?? string.Empty;

                if (firstSelection.FacultyId.HasValue)
                {
                    facultyCompulsorySubjects = await _dbContext.FacultyCompulsorySubjects
                        .AsNoTracking()
                        .Where(fc => fc.FacultyId == firstSelection.FacultyId.Value)
                        .Select(fc => fc.SubjectName ?? string.Empty)
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .ToListAsync(cancellationToken);
                }
            }

            // 3) Map to DTO manually (clean & explicit)
            var dto = new StudentApplicationDetailDto
            {
                // Basic
                ApplicationId = entity.ApplicationId,
                ApplicationNo = entity.ApplicationNo ?? string.Empty,
                ApplicationStatus = entity.ApplicationStatus ?? string.Empty,
                StudentName = entity.StudentName ?? string.Empty,
                FatherName = entity.FatherName ?? string.Empty,
                MotherName = entity.MotherName ?? string.Empty,
                RegistrationNo = entity.RegistrationNo ?? string.Empty,
                MobileNumber = entity.MobileNumber ?? string.Empty,
                DateOfBirth = entity.DateOfBirth,
                PermanentAddress = entity.PermanentAddress ?? string.Empty,
                LocalAddress = entity.LocalAddress ?? string.Empty,

                // Lookups
                ReligionName = entity.Religion?.ReligionName ?? string.Empty,
                CasteName = entity.Caste?.CasteName ?? string.Empty,
                CategoryName = entity.Category?.CategoryName ?? string.Empty,
                ClassName = entity.Class?.Name ?? string.Empty,
                GenderName = entity.Gender?.GenderName ?? string.Empty,
                BloodGroupName = entity.BloodGroup?.BloodGroupName ?? string.Empty,
                MaritalStatusName = entity.MaritalStatus?.MaritalStatusName ?? string.Empty,

                // Subjects
                FacultyName = facultyName,
                CompulsorySubjectName = compName,
                OptionalSubject1Name = opt1Name,
                OptionalSubject2Name = opt2Name,
                OptionalSubject3Name = opt3Name,
                AdditionalSubjectName = addName,
                FacultyCompulsorySubjects = facultyCompulsorySubjects,

                // Exam details
                ExamDetails = entity.StudentExamDetails
                    .Where(e => e.IsActive)
                    .OrderBy(e => e.YearOfPassing)
                    .Select(e => new ExamDetailItemDto
                    {
                        ExamName = e.ExamName ?? string.Empty,
                        SchoolCollege = e.SchoolCollege ?? string.Empty,
                        BoardCouncil = e.BoardCouncil ?? string.Empty,
                        YearOfPassing = e.YearOfPassing,
                        DivisionOrRank = e.DivisionOrRank ?? string.Empty
                    })
                    .ToList(),

                // Certificates
                Certificates = entity.StudentCertificates
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.CertificateId)
                    .Select(c => new CertificateItemDto
                    {
                        CertificateType = c.CertificateType ?? string.Empty,
                        CertificateNumber = c.CertificateNumber ?? string.Empty,
                        IssueDate = c.IssueDate,
                        IssuedBy = c.IssuedBy ?? string.Empty,
                        FilePath = c.FilePath ?? string.Empty
                    })
                    .ToList()
            };

            return ResponseDto.Success(dto);
        }
    }
}
