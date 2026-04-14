using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Api.CORS.GetApplicationPdf;
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

        // GET: api/student/applications/by-application-no?applicationNo=AP2026
        [HttpGet]
        [Route("applications/by-application-no")]
        public async Task<IActionResult> GetApplicationsByApplicationNo([FromQuery] string applicationNo)
        {
            var result = await _mediator.Send(new GetStudentsByApplicationNoQuery(applicationNo));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/student/applications/by-registration-no?registrationNo=REG001
        [HttpGet]
        [Route("applications/by-registration-no")]
        public async Task<IActionResult> GetApplicationsByRegistrationNo([FromQuery] string registrationNo)
        {
            var result = await _mediator.Send(new GetStudentsByRegistrationNoQuery(registrationNo));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/student/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        // GET: api/student/application-pdf/{applicationId}
        [HttpGet("application-pdf/{applicationId:int}")]
        public async Task<IActionResult> GetApplicationPdf(int applicationId)
        {
            // use MediatR query that you already have
            var query = new GetApplicationPdfQuery(applicationId);

            try
            {
                var pdfBytes = await _mediator.Send(query);

                if (pdfBytes == null || pdfBytes.Length == 0)
                    return NotFound("PDF could not be generated.");

                // you can change file name if you want
                var fileName = $"Application_{applicationId}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (InvalidOperationException ex)
            {
                // e.g. "Application not found."
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error while generating PDF.");
            }
        }
    }
}
