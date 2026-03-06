using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Api.CQRS.Command;
using User.Api.DTOs;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MasterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> CreateRole(RoleDto role)
        {
            var result = await _mediator.Send(new AddRoleCommand(role));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
           
        }
    }
}
