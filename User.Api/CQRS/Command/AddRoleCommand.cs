using MediatR;
using User.Api.DTOs;

namespace User.Api.CQRS.Command
{
  
    public class AddRoleCommand : IRequest<ResponseDto>
    {
        public RoleDto AdmissionDto { get; set; }

        public AddRoleCommand(RoleDto admissionDto)
        {
            AdmissionDto = admissionDto;
        }
    }
}
