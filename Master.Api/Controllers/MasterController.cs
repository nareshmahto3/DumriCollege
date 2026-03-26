using LibraryService.Utility.Data.Core.DTOs;
using Master.Api.CQRS.Class;
using Master.Api.CQRS.Class.AddClass;
using Master.Api.CQRS.Class.GetClass;
using Master.Api.CQRS.Class.UpdateClass;
using Master.Api.CQRS.Course.AddCourse;
using Master.Api.CQRS.Course.DeleteCourse;
using Master.Api.CQRS.Course.GetAllCourse;
using Master.Api.CQRS.Course.UpdateCourse;
using Master.Api.CQRS.DeleteClass;
using Master.Api.CQRS.Grade.AddGrade;
using Master.Api.CQRS.Grade.DeleteGrade;
using Master.Api.CQRS.Grade.getGrade;
using Master.Api.CQRS.Grade.UpdateGrade;
using Master.Api.CQRS.Role;
using Master.Api.CQRS.Users;
using Master.Api.CQRS.Users.DeleteUser;
using Master.Api.CQRS.Users.UpdateUser;
using Master.Api.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Master.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateUserCommandHandler _handler;
        private readonly DeleteUserCommandHandler _deleteHandler;
        public MasterController(IMediator mediator, UpdateUserCommandHandler updateHandler, DeleteUserCommandHandler deleteHandler)
        {
            _mediator = mediator;
            _handler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        //course controller
        [HttpPost]
        [Route("AddNewCourse")]
        public async Task<IActionResult> AddCourse(CourseDto product)
        {
            var result = await _mediator.Send(new AddCourseCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllcourseQuery(filter));
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(CourseDto product)
        {
            var result = await _mediator.Send(new UpdateCourseCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpDelete("DeleteCourse{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var command = new DeleteCourseCommand { CourseId = id };

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }






        //class controller
        [HttpPost]
        [Route("AddNewClass")]
        public async Task<IActionResult> AddClass(ClassDto product)
        {
            var result = await _mediator.Send(new AddClassCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }



        //[HttpGet]
        //[Route("GetAllClass")]
        //public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        //{
        //    var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
        //    var result = await _mediator.Send(new GetClassCommand(filter));
        //    return Ok(result);
        //}
        [HttpGet]
        [Route("GetAllClass")]
        public async Task<IActionResult> GetAllClass([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetClassCommand(filter));
            return Ok(result);
        }

        [HttpDelete("DeleteClass{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteClassCommand { ClassId = id };

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut]
        [Route("UpdateClass")]
        public async Task<IActionResult> UpdateProduct(ClassDto product)
        {
            var result = await _mediator.Send(new UpdateClassCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }


        //Grade controller

        [HttpPost]
        [Route("AddNewGrade")]
        public async Task<IActionResult> AddGrade(GradeDto product)
        {
            var result = await _mediator.Send(new AddGradeCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetAllGrade")]
        public async Task<IActionResult> GetAllGrade([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetGradeCommand(filter));
            return Ok(result);
        }

        [HttpDelete("DeleteGrade{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var command = new DeleteGradeCommand { GradeId = id };

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpPut]
        [Route("UpdateGrade")]
        public async Task<IActionResult> UpdateGrade(GradeDto product)
        {
            var result = await _mediator.Send(new UpdateGradeCommand(product));
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }


        //roles and users controller will be added here in future


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var id = await _mediator.Send(new CreateUserCommand(dto));
            return Ok(id);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserCommand command)
        {
            if (userId != command.UserId)
                return BadRequest("UserId in URL does not match UserId in body.");

            try
            {
                // ✅ Use the injected handler instead of creating a new one
                await _handler.Handle(command);
                return Ok(new { Message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _deleteHandler.Handle(new DeleteUserCommand { UserId = id });
                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }


}

