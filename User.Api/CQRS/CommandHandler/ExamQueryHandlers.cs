using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace User.Api.CQRS.CommandHandler
{
    public class GetExamByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetExamByIdQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var examRepo = unitOfWork.Repository<User.Api.DbEntities.Exam>();
            var exam = (await examRepo.FindAsync(e => e.Id == request.Id && e.IsActive)).FirstOrDefault();

            if (exam == null)
                return ResponseDto.Fail("Exam not found");

            // Fetch exam subjects separately
            var examSubjectRepo = unitOfWork.Repository<User.Api.DbEntities.ExamSubject>();
            var examSubjects = await examSubjectRepo.FindAsync(es => es.ExamId == request.Id);

            var examDto = new ExamDto
            {
                Id = exam.Id,
                ExamName = exam.ExamName,
                ExamType = exam.ExamType,
                Class = exam.Class,
                AcademicYear = exam.AcademicYear,
                StartDate = exam.StartDate,
                EndDate = exam.EndDate,
                Venue = exam.Venue,
                Instructions = exam.Instructions,
                CreatedAt = exam.CreatedAt,
                ExamSubjects = examSubjects.Select(es => new ExamSubjectDto
                {
                    Id = es.Id,
                    ExamId = es.ExamId,
                    Subject = es.Subject,
                    Date = es.Date,
                    StartTime = es.StartTime,
                    EndTime = es.EndTime,
                    MaxMarks = es.MaxMarks,
                    CreatedAt = es.CreatedAt
                }).ToList()
            };

            return ResponseDto.Success(examDto, "Exam retrieved successfully");
        }
    }

    public class GetAllExamsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllExamsQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var examRepo = unitOfWork.Repository<User.Api.DbEntities.Exam>();

            var pagedResult = await examRepo.GetPagedAsync(
                e => e.IsActive &&
                     (string.IsNullOrWhiteSpace(request.Class) || e.Class == request.Class) &&
                     (string.IsNullOrWhiteSpace(request.AcademicYear) || e.AcademicYear == request.AcademicYear) &&
                     (string.IsNullOrWhiteSpace(request.ExamType) || e.ExamType == request.ExamType),
                request.PageNumber,
                request.PageSize
            );

            var examDtos = pagedResult.Data.Select(exam => new ExamDto
            {
                Id = exam.Id,
                ExamName = exam.ExamName,
                ExamType = exam.ExamType,
                Class = exam.Class,
                AcademicYear = exam.AcademicYear,
                StartDate = exam.StartDate,
                EndDate = exam.EndDate,
                Venue = exam.Venue,
                Instructions = exam.Instructions,
                CreatedAt = exam.CreatedAt
            }).ToList();

            var result = new PagedResponse<ExamDto>(
                examDtos,
                pagedResult.TotalRecords,
                request.PageNumber,
                request.PageSize
            );

            return ResponseDto.Success(result, "Exams retrieved successfully");
        }
    }
}
