using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class AddTeacherFormCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddTeacherFormCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddTeacherFormCommand request, CancellationToken cancellationToken)
        {
            if (request.TeacherFormDto == null)
                return ResponseDto.Fail("Teacher data is null");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.TeacherFormDto.EmployeeId))
                return ResponseDto.Fail("Employee ID is required");

            if (string.IsNullOrWhiteSpace(request.TeacherFormDto.FirstName))
                return ResponseDto.Fail("First Name is required");

            if (string.IsNullOrWhiteSpace(request.TeacherFormDto.Email))
                return ResponseDto.Fail("Email is required");

            if (string.IsNullOrWhiteSpace(request.TeacherFormDto.Phone))
                return ResponseDto.Fail("Phone is required");

            // Handle photo upload if provided
            string? photoPath = null;
            if (request.TeacherFormDto.Photo != null && request.TeacherFormDto.Photo.Length > 0)
            {
                // Validate file size (max 5MB)
                if (request.TeacherFormDto.Photo.Length > 5 * 1024 * 1024)
                    return ResponseDto.Fail("Photo size must not exceed 5MB");

                // Validate file type
                var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (!allowedMimeTypes.Contains(request.TeacherFormDto.Photo.ContentType))
                    return ResponseDto.Fail("Only JPEG, PNG, GIF, and WebP images are allowed");

                try
                {
                    // Save photo to disk
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "teachers");
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    var fileName = $"{Guid.NewGuid()}_{request.TeacherFormDto.Photo.FileName}";
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.TeacherFormDto.Photo.CopyToAsync(stream, cancellationToken);
                    }

                    photoPath = Path.Combine("uploads", "teachers", fileName).Replace("\\", "/");
                }
                catch (Exception ex)
                {
                    return ResponseDto.Fail($"Error uploading photo: {ex.Message}");
                }
            }

            // Map DTO to Entity
            var teacher = new User.Api.DbEntities.Teacher
            {
                EmployeeId = request.TeacherFormDto.EmployeeId,
                FirstName = request.TeacherFormDto.FirstName,
                LastName = request.TeacherFormDto.LastName,
                Email = request.TeacherFormDto.Email,
                Phone = request.TeacherFormDto.Phone,
                DateOfBirth = request.TeacherFormDto.DateOfBirth,
                Gender = request.TeacherFormDto.Gender,
                Qualification = request.TeacherFormDto.Qualification,
                Designation = request.TeacherFormDto.Designation,
                Department = request.TeacherFormDto.Department,
                JoiningDate = request.TeacherFormDto.JoiningDate,
                Experience = request.TeacherFormDto.Experience,
                Salary = request.TeacherFormDto.Salary,
                Address = request.TeacherFormDto.Address,
                City = request.TeacherFormDto.City,
                State = request.TeacherFormDto.State,
                ZipCode = request.TeacherFormDto.ZipCode,
                BloodGroup = request.TeacherFormDto.BloodGroup,
                Religion = request.TeacherFormDto.Religion,
                EmergencyContact = request.TeacherFormDto.EmergencyContact,
                Subjects = request.TeacherFormDto.Subjects,
                ShortBio = request.TeacherFormDto.ShortBio,
                PhotoPath = photoPath,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
            await teacherRepo.AddAsync(teacher);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: teacher.Id, message: "Teacher added successfully");
        }
    }
}
