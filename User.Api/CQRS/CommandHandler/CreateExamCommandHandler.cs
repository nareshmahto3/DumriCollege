using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class CreateExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateExamCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            if (request.ExamDto == null)
                return ResponseDto.Fail("Exam data is null");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.ExamDto.ExamName))
                return ResponseDto.Fail("Exam name is required");

            if (string.IsNullOrWhiteSpace(request.ExamDto.ExamType))
                return ResponseDto.Fail("Exam type is required");

            if (string.IsNullOrWhiteSpace(request.ExamDto.Class))
                return ResponseDto.Fail("Class is required");

            if (string.IsNullOrWhiteSpace(request.ExamDto.AcademicYear))
                return ResponseDto.Fail("Academic year is required");

            if (string.IsNullOrWhiteSpace(request.ExamDto.Venue))
                return ResponseDto.Fail("Venue is required");

            if (request.ExamDto.ExamSubjects == null || !request.ExamDto.ExamSubjects.Any())
                return ResponseDto.Fail("At least one subject is required");

            // Validate exam subjects
            foreach (var subject in request.ExamDto.ExamSubjects)
            {
                if (string.IsNullOrWhiteSpace(subject.Subject))
                    return ResponseDto.Fail("Subject name is required for all exam subjects");

                if (subject.MaxMarks <= 0)
                    return ResponseDto.Fail("Max marks must be greater than 0");
            }

            // Create exam entity
            var exam = new User.Api.DbEntities.Exam
            {
                ExamName = request.ExamDto.ExamName,
                ExamType = request.ExamDto.ExamType,
                Class = request.ExamDto.Class,
                AcademicYear = request.ExamDto.AcademicYear,
                StartDate = request.ExamDto.StartDate,
                EndDate = request.ExamDto.EndDate,
                Venue = request.ExamDto.Venue,
                Instructions = request.ExamDto.Instructions,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var examRepo = unitOfWork.Repository<User.Api.DbEntities.Exam>();
            await examRepo.AddAsync(exam);
            await unitOfWork.SaveChangesAsync();

            // Create exam subjects
            var examSubjectRepo = unitOfWork.Repository<User.Api.DbEntities.ExamSubject>();
            foreach (var subjectDto in request.ExamDto.ExamSubjects)
            {
                var examSubject = new User.Api.DbEntities.ExamSubject
                {
                    ExamId = exam.Id,
                    Subject = subjectDto.Subject,
                    Date = subjectDto.Date,
                    StartTime = subjectDto.StartTime,
                    EndTime = subjectDto.EndTime,
                    MaxMarks = subjectDto.MaxMarks,
                    CreatedAt = DateTime.UtcNow
                };
                await examSubjectRepo.AddAsync(examSubject);
            }

            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(exam.Id, "Exam scheduled successfully");
        }
    }
}
