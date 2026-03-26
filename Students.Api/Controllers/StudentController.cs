using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Api.CORS.RegisterStudent;
using Students.Api.CORS.Student;

namespace Students.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // existing endpoints...

        // GET: api/student/applications
        [HttpGet]
        [Route("applications")]
        public async Task<IActionResult> GetApplications()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/student/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
