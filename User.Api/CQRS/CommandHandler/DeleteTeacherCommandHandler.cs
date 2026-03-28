using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class DeleteTeacherCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTeacherCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            var teachers = await teacherRepo.FindAsync(t => t.Id == request.Id);
            var existingTeacher = teachers.FirstOrDefault();

            if (existingTeacher == null)
                return ResponseDto.Fail("Teacher not found");

            teacherRepo.Remove(existingTeacher);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: null, message: "Teacher deleted successfully");
        }
    }
}
