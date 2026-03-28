using MediatR;
using User.Api.DTOs;

namespace User.Api.CQRS.Command
{
    public class GetAllCertificatesQuery : IRequest<ResponseDto>
    {
    }
}
