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
    public class ClassController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add a new class
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody] AddClassDto classDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddClassCommand(classDto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetClassById), new { id = result.Data }, result);
        }

        /// <summary>
        /// Get class by ID
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            // TODO: Implement GetClassByIdQuery
            return Ok(new { Message = "GetClassById not implemented yet", Id = id });
        }

        /// <summary>
        /// Get all classes
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            // TODO: Implement GetAllClassesQuery
            return Ok(new { Message = "GetAllClasses not implemented yet" });
        }
    }
}