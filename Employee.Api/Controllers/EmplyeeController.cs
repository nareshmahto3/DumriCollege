using Employee.Api.CQRS.AddEmplyee;
using Employee.Api.CQRS.DeleteEmployee;
using Employee.Api.CQRS.GetEmployee;
using Employee.Api.CQRS.UpdateEmployee;
using Employee.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmplyeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("AddNew")]
        public async Task<IActionResult> AddProduct(EmployeeDto product)
        {
            var result = await _mediator.Send(new AddEmplyeeCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllEmployeeQuery(filter));
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct(EmployeeDto product)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));

            if (result != null)
                return NotFound("Employee not found");

            return Ok("Employee deleted successfully");
        }




    }

}

