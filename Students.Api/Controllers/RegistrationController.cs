using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.Api.CORS.DeleteStudent;
using Students.Api.CORS.RegisterStudent;
using Students.Api.CORS.UpdateStudent;
using Students.Api.DTOs;

namespace Students.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("RegisterStudent")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> RegisterStudent(
           [FromForm] StudentRegistrationDto registration,
           IFormFile? casteCertificateFile,
           IFormFile? schoolLeavingFile,
           IFormFile? admitCardFile,
           IFormFile? marksheetFile,
           IFormFile? aadhaarFile,
           IFormFile? photoFile)
        {
            var cmd = new RegisterStudentCommand(
                registration,
                casteCertificateFile,
                schoolLeavingFile,
                admitCardFile,
                marksheetFile,
                aadhaarFile,
                photoFile);

            var result = await _mediator.Send(cmd);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("UpdateStudent/{applicationId:int}")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> UpdateStudent(
            int applicationId,
            [FromForm] StudentRegistrationDto registration,
            IFormFile? casteCertificateFile,
            IFormFile? schoolLeavingFile,
            IFormFile? admitCardFile,
            IFormFile? marksheetFile,
            IFormFile? aadhaarFile,
            IFormFile? photoFile)
        {
            var cmd = new UpdateStudentCommand(
                applicationId,
                registration,
                casteCertificateFile,
                schoolLeavingFile,
                admitCardFile,
                marksheetFile,
                aadhaarFile,
                photoFile);

            var result = await _mediator.Send(cmd);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("DeleteStudent/{applicationId:int}")]
        public async Task<IActionResult> DeleteStudent(int applicationId)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(applicationId));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

