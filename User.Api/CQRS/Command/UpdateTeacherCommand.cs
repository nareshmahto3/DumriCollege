using MediatR;
using User.Api.DTOs;
using User.Api.Models;

namespace User.Api.CQRS.Command
{
    public class UpdateTeacherCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public AddTeacherModel TeacherDto { get; set; }

        public UpdateTeacherCommand(int id, AddTeacherModel teacherDto)
        {
            Id = id;
            TeacherDto = teacherDto;
        }
    }
}
