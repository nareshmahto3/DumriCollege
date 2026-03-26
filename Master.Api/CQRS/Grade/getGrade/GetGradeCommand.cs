using LibraryService.Utility.Data.Core.DTOs;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.getGrade
{
    public class GetGradeCommand : IRequest<PagedResponse<GradeDto>>
    {
        public PaginationFilter Filter { get; set; }
        public GetGradeCommand(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}


