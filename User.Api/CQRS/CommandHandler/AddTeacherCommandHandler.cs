using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class AddTeacherCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddTeacherCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddTeacherCommand  request, CancellationToken cancellationToken)
        {
            if (request.TeacherDto == null)
                return ResponseDto.Fail("Teacher data is null");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.TeacherDto.EmployeeId))
                return ResponseDto.Fail("Employee ID is required");

            if (string.IsNullOrWhiteSpace(request.TeacherDto.FirstName))
                return ResponseDto.Fail("First Name is required");

            if (string.IsNullOrWhiteSpace(request.TeacherDto.Email))
                return ResponseDto.Fail("Email is required");

            if (string.IsNullOrWhiteSpace(request.TeacherDto.Phone))
                return ResponseDto.Fail("Phone is required");

            // Map DTO to Entity
            var teacher = new User.Api.DbEntities.MTeacher
            {
                EmployeeId = request.TeacherDto.EmployeeId,
                FirstName = request.TeacherDto.FirstName,
                LastName = request.TeacherDto.LastName,
                Email = request.TeacherDto.Email,
                Phone = request.TeacherDto.Phone,
                //DateOfBirth = request.TeacherDto.DateOfBirth,
                Gender = request.TeacherDto.Gender,
                Qualification = request.TeacherDto.Qualification,
                Designation = request.TeacherDto.Designation,
                Department = request.TeacherDto.Department,
                //JoiningDate = request.TeacherDto.JoiningDate,
                Experience = request.TeacherDto.Experience,
                Salary = request.TeacherDto.Salary,
                Address = request.TeacherDto.Address,
                City = request.TeacherDto.City,
                State = request.TeacherDto.State,
                ZipCode = request.TeacherDto.ZipCode,
                BloodGroup = request.TeacherDto.BloodGroup,
                Religion = request.TeacherDto.Religion,
                EmergencyContact = request.TeacherDto.EmergencyContact,
                Subjects = request.TeacherDto.Subjects,
                ShortBio = request.TeacherDto.ShortBio,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.MTeacher>();
            await teacherRepo.AddAsync(teacher);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: teacher.Id, message: "Teacher added successfully");
        }
    }
}
