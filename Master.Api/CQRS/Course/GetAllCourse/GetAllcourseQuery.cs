using LibraryService.Utility.Data.Core.DTOs;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.GetAllCourse
{
    public class GetAllcourseQuery : IRequest<PagedResponse<CourseDto>>
    {
        public PaginationFilter Filter { get; set; }
        public GetAllcourseQuery(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}


