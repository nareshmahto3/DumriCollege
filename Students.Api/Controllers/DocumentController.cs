using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.Api.CORS.Document;
using Students.Api.DTOs;

namespace Students.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/document/{applicationId}
        [HttpGet("{applicationId:int}")]
        public async Task<IActionResult> GetDocuments(int applicationId)
        {
            var query = new GetDocumentsByApplicationIdQuery(applicationId);
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST /api/document/approve
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveDocument([FromBody] ApproveDocumentDto dto)
        {
            var cmd = new ApproveDocumentCommand(dto);
            var result = await _mediator.Send(cmd);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST /api/document/reject
        [HttpPost("reject")]
        public async Task<IActionResult> RejectDocument([FromBody] RejectDocumentDto dto)
        {
            var cmd = new RejectDocumentCommand(dto);
            var result = await _mediator.Send(cmd);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST /api/document/reupload
        [HttpPost("reupload")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> ReuploadDocument(
            [FromForm] ReuploadDocumentDto dto,
            IFormFile? file)
        {
            var cmd = new ReuploadDocumentCommand(dto, file);
            var result = await _mediator.Send(cmd);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
