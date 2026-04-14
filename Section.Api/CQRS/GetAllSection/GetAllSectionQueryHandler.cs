using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Section.Api.DTOs;

namespace Section.Api.CQRS.GetAllSection
{
    public class GetAllSectionQueryHandler(IRepository<Section.Api.DbEntities.Section> _service) : IRequestHandler<GetAllSectionQuery, PagedResponse<SectionDto>>
    {
        public async Task<PagedResponse<SectionDto>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new SectionDto
            {
                SectionId = a.SectionId,
                SectionName = a.SectionName,
                
            });
            return new PagedResponse<SectionDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}
