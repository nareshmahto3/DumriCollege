using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;

using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.GetAllCourse
{
    public class GetAllcourseQueryHandler(IRepository<CourseMaster> _service) : IRequestHandler<GetAllcourseQuery, PagedResponse<CourseDto>>
    {
        public async Task<PagedResponse<CourseDto>> Handle(GetAllcourseQuery request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new CourseDto
            {
                CourseId = a.CourseId,
                CourseName = a.CourseName,
                DurationYears = a.DurationYears,
                IsActive = a.IsActive,
                CreatedDate = a.CreatedDate

            });
            return new PagedResponse<CourseDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}




