using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class GetAllTeachersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTeachersQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            var teachers = await teacherRepo.GetAllAsync();

            if (teachers == null || !teachers.Any())
                return ResponseDto.Success(Data: new List<User.Api.DbEntities.Teacher>(), message: "No teachers found");

            return ResponseDto.Success(Data: teachers, message: "Teachers retrieved successfully");
        }
    }
}
