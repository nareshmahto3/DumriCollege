using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.UpdateGrade
{
    public class UpdateGradeCommand : IRequest<ResponseDto>
    {
        public GradeDto GradeDto { get; set; }

        public UpdateGradeCommand(GradeDto gradeDto)
        {
            GradeDto = gradeDto;
        }

    }
}