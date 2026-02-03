using Admission.Api.DTOs;
using MediatR;
using Admission.Api.CQRS.GetProduct;
using Admission.Api.DbEntities;
using LibraryService.Utility.Data.Core.Interfaces;
namespace Admission.Api.CQRS.GetAdmission
{
    public class GetAdmissionByIdQueryHandler(IRepository<Admission.Api.DbEntities.Admission> service) : IRequestHandler<GetAdmissionByIdQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetAdmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await service.GetByIdAsync(request.AdmissionId);
            return ResponseDto.Success(result);
        }
    }
}
