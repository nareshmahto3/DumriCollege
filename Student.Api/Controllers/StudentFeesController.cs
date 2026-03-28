using MediatR;
using Microsoft.AspNetCore.Mvc;
using Student.Api.CQRS.Commands;
using Student.Api.CQRS.Queries;
using Student.Api.DTOs;

namespace Student.Api.Controllers
{
    [ApiController]
    [Route("student/fees")]
    public class StudentFeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentFeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentFees(int studentId)
        {
            var response = await _mediator.Send(new GetStudentFeesQuery(studentId));
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayStudentFees([FromBody] StudentFeePaymentRequestDto request)
        {
            var response = await _mediator.Send(new PayStudentFeesCommand(request));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
