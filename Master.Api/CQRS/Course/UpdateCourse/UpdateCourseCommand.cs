using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<ResponseDto>
    {
        public CourseDto CourseDto { get; set; }

        public UpdateCourseCommand(CourseDto courseDto)
        {
            CourseDto = courseDto;
        }

    }
}

