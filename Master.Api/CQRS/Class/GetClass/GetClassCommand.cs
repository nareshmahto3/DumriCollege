using LibraryService.Utility.Data.Core.DTOs;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.GetClass
{
    public class GetClassCommand : IRequest<PagedResponse<ClassDto>>
    {
        public PaginationFilter Filter { get; set; }
        public GetClassCommand(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}


