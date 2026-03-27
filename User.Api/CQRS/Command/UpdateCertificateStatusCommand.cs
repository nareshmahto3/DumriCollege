using MediatR;

namespace User.Api.CQRS.Command
{
    public class UpdateCertificateStatusCommand : IRequest<ResponseDto>
    {
        public int DocumentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }

        public UpdateCertificateStatusCommand(int documentId, string status, string? remarks = null)
        {
            DocumentId = documentId;
            Status = status;
            Remarks = remarks;
        }
    }
}
