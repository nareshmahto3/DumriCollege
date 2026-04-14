using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.Api.CORS.Subjects;

namespace Students.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Subject/Faculties
        [HttpGet]
        [Route("Faculties")]
        public async Task<IActionResult> GetFaculties()
        {
            var result = await _mediator.Send(new GetFacultiesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/Subject/Classes
        [HttpGet]
        [Route("Classes")]
        public async Task<IActionResult> GetClasses()
        {
            var result = await _mediator.Send(new GetClassesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/Subject/OptionalByFaculty/2
        [HttpGet]
        [Route("OptionalByFaculty/{facultyId:int}")]
        public async Task<IActionResult> GetOptionalByFaculty(int facultyId)
        {
            var result = await _mediator.Send(new GetOptionalSubjectsByFacultyQuery(facultyId));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/Subject/Additional
        [HttpGet]
        [Route("Additional")]
        public async Task<IActionResult> GetAdditional()
        {
            var result = await _mediator.Send(new GetAdditionalSubjectsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // GET: api/Subject/Compulsory
        [HttpGet]
        [Route("Compulsory")]
        public async Task<IActionResult> GetCompulsory()
        {
            var result = await _mediator.Send(new GetCompulsorySubjectsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("FacultyCompulsory/{facultyId:int}")]
        public async Task<IActionResult> GetFacultyCompulsory(int facultyId)
        {
            var result = await _mediator.Send(new GetFacultyCompulsorySubjectsQuery(facultyId));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("Religions")]
        public async Task<IActionResult> GetReligions()
        {
            var result = await _mediator.Send(new GetReligionsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("Castes")]
        public async Task<IActionResult> GetCastes()
        {
            var result = await _mediator.Send(new GetCastesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("Genders")]
        public async Task<IActionResult> GetGenders()
        {
            var result = await _mediator.Send(new GetGendersQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("BloodGroups")]
        public async Task<IActionResult> GetBloodGroups()
        {
            var result = await _mediator.Send(new GetBloodGroupsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("MaritalStatuses")]
        public async Task<IActionResult> GetMaritalStatuses()
        {
            var result = await _mediator.Send(new GetMaritalStatusesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
    

