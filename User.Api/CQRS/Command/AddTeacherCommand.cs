using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class AddTeacherCommand : IRequest<ResponseDto>
    {
        public AddTeacherDto TeacherDto { get; set; }

        public AddTeacherCommand(AddTeacherDto teacherDto)
        {
            TeacherDto = teacherDto;
        }
    }
}
