using MediatR;
using Students.Api.Services;

namespace Students.Api.CORS.GetApplicationPdf
{
    public class GetApplicationPdfQueryHandler
        : IRequestHandler<GetApplicationPdfQuery, byte[]>
    {
        private readonly IStudentPdfService _pdfService;

        public GetApplicationPdfQueryHandler(IStudentPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<byte[]> Handle(GetApplicationPdfQuery request, CancellationToken cancellationToken)
        {
            return await _pdfService.GenerateStudentApplicationPdf(request.ApplicationId);
        }
    }
}
