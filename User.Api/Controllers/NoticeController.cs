using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Api.CQRS.Command;
using User.Api.DTOs;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoticeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateNotice([FromForm] CreateNoticeDto noticeDto)
        {
            var command = new CreateNoticeCommand(noticeDto);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return CreatedAtAction(nameof(GetNoticeById), new { id = result.Data?.Id }, result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateNotice(int id, [FromForm] UpdateNoticeDto noticeDto)
        {
            noticeDto.Id = id;
            var command = new UpdateNoticeCommand(noticeDto);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotice(int id)
        {
            var command = new DeleteNoticeCommand(id);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticeById(int id)
        {
            var query = new GetNoticeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotices(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? category = null,
            [FromQuery] string? priority = null)
        {
            var query = new GetAllNoticesQuery(pageNumber, pageSize, searchTerm, category, priority);
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}