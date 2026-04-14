  using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.AddClass
{
    public class AddClassCommand : IRequest<ResponseDto>
    {
        public ClassDto ClassDto { get; set; }

        public AddClassCommand(ClassDto classDto)
        {
            ClassDto = classDto;
        }
    }

}


