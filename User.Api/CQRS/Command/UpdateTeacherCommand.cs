using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class UpdateTeacherCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public AddTeacherDto TeacherDto { get; set; }

        public UpdateTeacherCommand(int id, AddTeacherDto teacherDto)
        {
            Id = id;
            TeacherDto = teacherDto;
        }
    }
}
