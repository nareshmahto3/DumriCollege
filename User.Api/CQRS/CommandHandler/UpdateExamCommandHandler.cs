using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class UpdateExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateExamCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            if (request.ExamDto == null)
                return ResponseDto.Fail("Exam data is null");

            var examRepo = unitOfWork.Repository<User.Api.DbEntities.Exam>();
            var exam = await examRepo.GetByIdAsync(request.Id);

            if (exam == null)
                return ResponseDto.Fail("Exam not found");

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

            // Update exam
            exam.ExamName = request.ExamDto.ExamName;
            exam.ExamType = request.ExamDto.ExamType;
            exam.Class = request.ExamDto.Class;
            exam.AcademicYear = request.ExamDto.AcademicYear;
            exam.StartDate = request.ExamDto.StartDate;
            exam.EndDate = request.ExamDto.EndDate;
            exam.Venue = request.ExamDto.Venue;
            exam.Instructions = request.ExamDto.Instructions;
            exam.UpdatedAt = DateTime.UtcNow;

            examRepo.Update(exam);

            // Handle exam subjects - remove existing and add new ones
            var examSubjectRepo = unitOfWork.Repository<User.Api.DbEntities.ExamSubject>();
            var existingSubjects = await examSubjectRepo.FindAsync(es => es.ExamId == request.Id);

            // Remove existing subjects
            foreach (var subject in existingSubjects)
            {
                examSubjectRepo.Remove(subject);
            }

            // Add updated subjects
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

            return ResponseDto.Success(null, "Exam updated successfully");
        }
    }
}
