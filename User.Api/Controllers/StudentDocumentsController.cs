using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Api.CQRS.Command;
using User.Api.DTOs;

namespace User.Api.Controllers
{
    [ApiController]
    public class StudentDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("student/documents")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadDocuments([FromForm] string studentId, [FromForm] string documentType, [FromForm] List<IFormFile> files)
        {
            var command = new UploadStudentDocumentsCommand(studentId, documentType, files);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Created("student/documents", result);
        }

        [HttpGet("admin/certificates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCertificates()
        {
            var query = new GetAllCertificatesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("admin/certificates/{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCertificateStatus(int id, [FromBody] UpdateCertificateStatusCommand command)
        {
            if (id != command.DocumentId)
            {
                return BadRequest(ResponseDto.Fail("Document id mismatch"));
            }

            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
