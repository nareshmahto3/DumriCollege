using Employee.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using MediatR;

namespace Employee.Api.CQRS.GetEmployee
{
    public class GetAllEmployeeQuery : IRequest<PagedResponse<EmployeeDto>>
    {
        public PaginationFilter Filter { get; set; }
        public GetAllEmployeeQuery(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}