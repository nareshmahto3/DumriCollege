using MediatR;

namespace Students.Api.CORS.GetApplicationPdf
{
    public class GetApplicationPdfQuery : IRequest<byte[]>
    {
        public int ApplicationId { get; }

        public GetApplicationPdfQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
