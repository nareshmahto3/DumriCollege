using Admission.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Admission.Api.CQRS.AddExam;

public class AddExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddExamCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(AddExamCommand request, CancellationToken cancellationToken)
    {
        if (request.ExamDto == null)
            return ResponseDto.Fail("Exam data is null");

        var exam = new Admission.Api.DbEntities.Exam
        {
            ExamName = request.ExamDto.ExamName,
            ExamType = request.ExamDto.ExamType,
            Class = request.ExamDto.Class,
            AcademicYear = request.ExamDto.AcademicYear,
            StartDate = request.ExamDto.StartDate,
            EndDate = request.ExamDto.EndDate,
            Venue = request.ExamDto.Venue,
            Instructions = request.ExamDto.Instructions,
            CreatedAt = DateTime.UtcNow
        };

        var examRepo = unitOfWork.Repository<Admission.Api.DbEntities.Exam>();
        await examRepo.AddAsync(exam);
        await unitOfWork.SaveChangesAsync();

        return ResponseDto.Success(exam, "Exam scheduled successfully");
    }
}
