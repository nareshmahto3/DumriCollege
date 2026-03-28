using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Api.CQRS.Command;
using User.Api.DTOs;
using MediatR;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add a new subject
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] AddSubjectDto subjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddSubjectCommand(subjectDto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetSubjectById), new { id = result.Data }, result);
        }

        /// <summary>
        /// Get subject by ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            // TODO: Implement GetSubjectByIdQuery
            return Ok(new { Message = "GetSubjectById not implemented yet", Id = id });
        }

        /// <summary>
        /// Get all subjects
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            // TODO: Implement GetAllSubjectsQuery
            return Ok(new { Message = "GetAllSubjects not implemented yet" });
        }
    }
}