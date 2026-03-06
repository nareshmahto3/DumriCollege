using Admission.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using MediatR;

namespace Admission.Api.CQRS.GetAllAdmission
{
    public class GetAllAdmissionQuery : IRequest<PagedResponse<AdmissionDto>>
    {        public PaginationFilter Filter { get; set; }
        public GetAllAdmissionQuery(PaginationFilter pageFilter)
        {
            Filter = pageFilter;
        }
    }
}
