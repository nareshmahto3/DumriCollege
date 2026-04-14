using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.AddGrade
{
    public class AddGradeCommand : IRequest<ResponseDto>
    {
        public GradeDto GradeDto { get; set; }

        public AddGradeCommand(GradeDto gradeDto)
        {
            GradeDto = gradeDto;
        }
    }

}


