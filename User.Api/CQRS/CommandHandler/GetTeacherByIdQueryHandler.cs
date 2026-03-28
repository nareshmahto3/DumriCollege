using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class GetTeacherByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTeacherByIdQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            var teachers = await teacherRepo.FindAsync(t => t.Id == request.Id);
            var teacher = teachers.FirstOrDefault();

            if (teacher == null)
                return ResponseDto.Fail("Teacher not found");

            return ResponseDto.Success(Data: teacher, message: "Teacher retrieved successfully");
        }
    }
}
