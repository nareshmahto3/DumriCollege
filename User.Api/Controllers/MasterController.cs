using MediatR;
using Microsoft.AspNetCore.Mvc;
//using Employee.Api.Infrastructures;
using User.Api.CQRS.Command;

namespace User.Api.Controllers
{
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

        //public MasterController(EmployeeRepository repo, IMediator mediator)
        //{
        //    _repo = repo;
        //    _mediator = mediator;
        //}

        // ✅ Normal API (without CQRS)
        //[HttpGet("all-employees")]
        //public async Task<IActionResult> GetEmployee()
        //{
        //    var data = await _repo.GetEmployee();
        //    return Ok(data);
        //}

        // ✅ Dynamic Table API (CQRS)
        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetTableData(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return BadRequest("Table name is required");

            var result = await _mediator.Send(new MasterCommand(tableName));

            return Ok(result);
        }
    }
}