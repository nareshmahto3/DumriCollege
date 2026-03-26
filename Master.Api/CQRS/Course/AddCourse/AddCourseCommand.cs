using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.AddCourse
{
    public class AddCourseCommand : IRequest<ResponseDto>
    {
        public CourseDto CourseDto { get; set; }

        public AddCourseCommand(CourseDto courseDto)
        {
            CourseDto = courseDto;
        }
    }

}


