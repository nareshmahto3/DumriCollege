using Admission.Api.DTOs;
using MediatR;

namespace Admission.Api.CQRS.AddExam;

public class AddExamCommand : IRequest<ResponseDto>
{
    public ExamDto ExamDto { get; set; }
    public AddExamCommand(ExamDto examDto)
    {
        ExamDto = examDto;
    }
}
