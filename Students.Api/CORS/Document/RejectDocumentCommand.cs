using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Document
{
    public class RejectDocumentCommand : IRequest<ResponseDto>
    {
        public RejectDocumentDto Dto { get; }

        public RejectDocumentCommand(RejectDocumentDto dto)
        {
            Dto = dto;
        }
    }
}
