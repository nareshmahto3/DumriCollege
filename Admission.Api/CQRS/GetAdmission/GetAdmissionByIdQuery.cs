using Admission.Api.DTOs;
using MediatR;

namespace Admission.Api.CQRS.GetProduct
{
    public class GetAdmissionByIdQuery : IRequest<ResponseDto>
    {
        public int AdmissionId { get; }

        public GetAdmissionByIdQuery(int admissionId)
        {
            AdmissionId = admissionId;
        }
    }
}
