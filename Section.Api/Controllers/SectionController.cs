using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Section.Api.CQRS.GetAllSection;

namespace Section.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SectionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllSectionQuery(filter));
            return Ok(result);
        }


    }
}
