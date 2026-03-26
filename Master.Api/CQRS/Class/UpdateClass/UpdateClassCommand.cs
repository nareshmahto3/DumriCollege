using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.UpdateClass
{
    public class UpdateClassCommand : IRequest<ResponseDto>
    {
        public ClassDto ClassDto { get; set; }

        public UpdateClassCommand(ClassDto classDto)
        {
            ClassDto = classDto;
        }

    }
}

