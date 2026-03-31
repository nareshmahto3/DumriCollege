using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class UpdateTeacherCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTeacherCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request.TeacherDto == null)
                return ResponseDto.Fail("Teacher data is null");

            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            var teachers = await teacherRepo.FindAsync(t => t.Id == request.Id);
            var existingTeacher = teachers.FirstOrDefault();

            if (existingTeacher == null)
                return ResponseDto.Fail("Teacher not found");

            // Update properties
            //existingTeacher.FirstName = request.TeacherDto.FirstName;
            //existingTeacher.LastName = request.TeacherDto.LastName;
            //existingTeacher.Email = request.TeacherDto.Email;
            //existingTeacher.Phone = request.TeacherDto.Phone;
            //existingTeacher.DateOfBirth = request.TeacherDto.DateOfBirth;
            //existingTeacher.Gender = request.TeacherDto.Gender;
            //existingTeacher.Qualification = request.TeacherDto.Qualification;
            //existingTeacher.Designation = request.TeacherDto.Designation;
            //existingTeacher.Department = request.TeacherDto.Department;
            //existingTeacher.JoiningDate = request.TeacherDto.JoiningDate;
            //existingTeacher.Experience = request.TeacherDto.Experience;
            //existingTeacher.Salary = request.TeacherDto.Salary;
            //existingTeacher.Address = request.TeacherDto.Address;
            //existingTeacher.City = request.TeacherDto.City;
            //existingTeacher.State = request.TeacherDto.State;
            //existingTeacher.ZipCode = request.TeacherDto.ZipCode;
            //existingTeacher.BloodGroup = request.TeacherDto.BloodGroup;
            //existingTeacher.Religion = request.TeacherDto.Religion;
            //existingTeacher.EmergencyContact = request.TeacherDto.EmergencyContact;
            //existingTeacher.Subjects = request.TeacherDto.Subjects;
            //existingTeacher.ShortBio = request.TeacherDto.ShortBio;
            //existingTeacher.UpdatedAt = DateTime.UtcNow;

            teacherRepo.Update(existingTeacher);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: existingTeacher.Id, message: "Teacher updated successfully");
        }
    }
}
