using MediatR;
using Microsoft.AspNetCore.Http;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class ReuploadDocumentCommand : IRequest<ResponseDto>
    {
        public ReuploadDocumentDto Reupload { get; }
        public IFormFile? File { get; }

        public ReuploadDocumentCommand(ReuploadDocumentDto reupload, IFormFile? file)
        {
            Reupload = reupload;
            File = file;
        }
    }
}
