using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class GetDocumentsByApplicationIdQuery : IRequest<ResponseDto>
    {
        public int ApplicationId { get; }

        public GetDocumentsByApplicationIdQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
