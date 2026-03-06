using Admission.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Admission.Api.CQRS.GetAllAdmission
{
    public class GetAllAdmissionQueryHandler(IRepository<Admission.Api.DbEntities.Admission> _service) : IRequestHandler<GetAllAdmissionQuery, PagedResponse<AdmissionDto>>
    {
        public async Task<PagedResponse<AdmissionDto>> Handle(GetAllAdmissionQuery request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new AdmissionDto
            {
                Id = a.Id,
                AdmissionId = a.AdmissionId,
                Date = a.Date
            });
            return new PagedResponse<AdmissionDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}
