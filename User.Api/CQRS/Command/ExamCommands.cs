using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class CreateExamCommand : IRequest<ResponseDto>
    {
        public CreateExamDto ExamDto { get; set; }

        public CreateExamCommand(CreateExamDto examDto)
        {
            ExamDto = examDto;
        }
    }

    public class UpdateExamCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public UpdateExamDto ExamDto { get; set; }

        public UpdateExamCommand(int id, UpdateExamDto examDto)
        {
            Id = id;
            ExamDto = examDto;
        }
    }

    public class DeleteExamCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public DeleteExamCommand(int id)
        {
            Id = id;
        }
    }

    public class GetExamByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public GetExamByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetAllExamsQuery : IRequest<ResponseDto>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Class { get; set; }
        public string? AcademicYear { get; set; }
        public string? ExamType { get; set; }
    }
}
