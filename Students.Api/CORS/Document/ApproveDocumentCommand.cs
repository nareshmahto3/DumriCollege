using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class ApproveDocumentCommand : IRequest<ResponseDto>
    {
        public ApproveDocumentDto Dto { get; }

        public ApproveDocumentCommand(ApproveDocumentDto dto)
        {
            Dto = dto;
        }
    }
}
