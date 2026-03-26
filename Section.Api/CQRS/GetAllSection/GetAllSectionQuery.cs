using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using Section.Api.DTOs;

namespace Section.Api.CQRS.GetAllSection
{
    public class GetAllSectionQuery : IRequest<PagedResponse<SectionDto>>
    {
        public PaginationFilter Filter { get; set; }
        public GetAllSectionQuery(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}

