using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class AddTeacherCommand : IRequest<ResponseDto>
    {
        public Models.AddTeacherModel TeacherDto { get; set; }

        public AddTeacherCommand(Models.AddTeacherModel teacherDto)
        {
            TeacherDto = teacherDto;
        }
    }
}
