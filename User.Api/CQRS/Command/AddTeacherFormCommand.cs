using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class AddTeacherFormCommand : IRequest<ResponseDto>
    {
        public AddTeacherFormDto TeacherFormDto { get; set; }

        public AddTeacherFormCommand(AddTeacherFormDto teacherFormDto)
        {
            TeacherFormDto = teacherFormDto;
        }
    }
}
