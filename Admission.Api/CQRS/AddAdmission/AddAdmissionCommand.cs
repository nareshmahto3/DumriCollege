using Admission.Api.DTOs;
using MediatR;

namespace Admission.Api.CQRS.AddAdmission
{
    public class AddAdmissionCommand : IRequest<ResponseDto>
    {
        public AdmissionDto AdmissionDto { get; set; }

        public AddAdmissionCommand(AdmissionDto admissionDto)
        {
            AdmissionDto = admissionDto;
        }
    }

}
