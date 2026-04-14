using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.DeleteGrade
{
    public class DeleteGradeCommand : IRequest<ResponseDto>
    {
        public int GradeId { get; set; }
    }
}


