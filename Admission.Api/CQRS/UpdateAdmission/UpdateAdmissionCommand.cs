using Admission.Api.DTOs;
using MediatR;

namespace Admission.Api.CQRS.UpdateAdmission
{
    public class UpdateAdmissionCommand : IRequest<ResponseDto>
    {
        public AdmissionDto AdmissionDto { get; set; }

        public UpdateAdmissionCommand(AdmissionDto admissionDto)
        {
            AdmissionDto = admissionDto;
        }

    }
}

