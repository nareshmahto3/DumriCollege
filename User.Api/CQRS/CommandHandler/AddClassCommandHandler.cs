using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class AddClassCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddClassCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddClassCommand request, CancellationToken cancellationToken)
        {
            if (request.ClassDto == null)
                return ResponseDto.Fail("Class data is null");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.ClassDto.ClassName))
                return ResponseDto.Fail("Class Name is required");

            if (string.IsNullOrWhiteSpace(request.ClassDto.Section))
                return ResponseDto.Fail("Section is required");

            if (request.ClassDto.ClassTeacherId <= 0)
                return ResponseDto.Fail("Class Teacher is required");

            if (string.IsNullOrWhiteSpace(request.ClassDto.RoomNumber))
                return ResponseDto.Fail("Room Number is required");

            if (request.ClassDto.Capacity <= 0)
                return ResponseDto.Fail("Capacity must be greater than 0");

            if (string.IsNullOrWhiteSpace(request.ClassDto.AcademicYear))
                return ResponseDto.Fail("Academic Year is required");

            // Check if teacher exists
            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            var teacher = await teacherRepo.FindAsync(t => t.Id == request.ClassDto.ClassTeacherId && t.IsActive);
            if (teacher.FirstOrDefault() == null)
                return ResponseDto.Fail("Selected teacher does not exist or is inactive");

            // Map DTO to Entity
            var newClass = new User.Api.DbEntities.Class
            {
                ClassName = request.ClassDto.ClassName,
                Section = request.ClassDto.Section,
                ClassTeacherId = request.ClassDto.ClassTeacherId,
                RoomNumber = request.ClassDto.RoomNumber,
                Capacity = request.ClassDto.Capacity,
                AcademicYear = request.ClassDto.AcademicYear,
                StartDate = request.ClassDto.StartDate,
                Subjects = request.ClassDto.Subjects,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var classRepo = unitOfWork.Repository<User.Api.DbEntities.Class>();
            await classRepo.AddAsync(newClass);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: newClass.Id, message: "Class added successfully");
        }
    }
}