using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class GetTeacherByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public GetTeacherByIdQuery(int id)
        {
            Id = id;
        }
    }
}
