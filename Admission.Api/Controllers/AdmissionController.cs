using Admission.Api.CQRS.AddAdmission;
using Admission.Api.CQRS.GetAllAdmission;
using Admission.Api.CQRS.GetProduct;
using Admission.Api.CQRS.UpdateAdmission;
using Admission.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdmissionController : Controller
    {
        private readonly IMediator _mediator;
        public AdmissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetAdmissionByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllAdmissionQuery(filter));
            return Ok(result);
        }

        [HttpPost]
        [Route("AddNew")]
        public async Task<IActionResult> AddProduct(AdmissionDto product)
        {
            var result = await _mediator.Send(new AddAdmissionCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct(AdmissionDto product)
        {
            var result = await _mediator.Send(new UpdateAdmissionCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
    }
}
