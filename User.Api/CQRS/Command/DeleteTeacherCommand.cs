using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class DeleteTeacherCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public DeleteTeacherCommand(int id)
        {
            Id = id;
        }
    }
}
