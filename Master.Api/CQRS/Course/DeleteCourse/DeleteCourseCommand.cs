using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<ResponseDto>
    {
        public int CourseId { get; set; }
    }
}


