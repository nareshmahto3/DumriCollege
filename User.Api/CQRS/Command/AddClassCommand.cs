using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class AddClassCommand : IRequest<ResponseDto>
    {
        public AddClassDto ClassDto { get; set; }

        public AddClassCommand(AddClassDto classDto)
        {
            ClassDto = classDto;
        }
    }
}