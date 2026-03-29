using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Employee.Api.Infrastructures;
using User.Api.CQRS.Command;
using User.Api.CQRS.Query;
using User.Api.DTOs;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        //private readonly EmployeeRepository _repo;
        private readonly IMediator _mediator;

      

        public MasterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return BadRequest("Table name is required");
           
            var result = await _mediator.Send(new MasterCommand(tableName));

            return Ok(result);
        }
        #endregion
    }
}
