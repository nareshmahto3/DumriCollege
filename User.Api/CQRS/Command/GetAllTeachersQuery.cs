using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class GetAllTeachersQuery : IRequest<ResponseDto>
    {
    }
}
