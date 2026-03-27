using MediatR;

namespace User.Api.CQRS.Command
{
    public class GetAllCertificatesQuery : IRequest<ResponseDto>
    {
    }
}
