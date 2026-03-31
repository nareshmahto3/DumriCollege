using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    //public class AddSubjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddSubjectCommand, ResponseDto>
    //{
    //    public async Task<ResponseDto> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
    //    {
    //        if (request.SubjectDto == null)
    //            return ResponseDto.Fail("Subject data is null");

    //        // Validate required fields
    //        if (string.IsNullOrWhiteSpace(request.SubjectDto.SubjectName))
    //            return ResponseDto.Fail("Subject Name is required");

    //        if (string.IsNullOrWhiteSpace(request.SubjectDto.SubjectCode))
    //            return ResponseDto.Fail("Subject Code is required");

    //        if (request.SubjectDto.ClassId <= 0)
    //            return ResponseDto.Fail("Class is required");

    //        if (request.SubjectDto.TeacherId <= 0)
    //            return ResponseDto.Fail("Teacher is required");

    //        if (request.SubjectDto.Credits <= 0)
    //            return ResponseDto.Fail("Credits must be greater than 0");

    //        if (string.IsNullOrWhiteSpace(request.SubjectDto.Type))
    //            return ResponseDto.Fail("Subject Type is required");

    //        // Check if class exists
    //        var classRepo = unitOfWork.Repository<User.Api.DbEntities.Class>();
    //        var classEntity = await classRepo.FindAsync(c => c.Id == request.SubjectDto.ClassId && c.IsActive);
    //        if (classEntity.FirstOrDefault() == null)
    //            return ResponseDto.Fail("Selected class does not exist or is inactive");

    //        // Check if teacher exists
    //        var teacherRepo = unitOfWork.Repository<User.Api.DbEntities.Teacher>();
    //        var teacher = await teacherRepo.FindAsync(t => t.Id == request.SubjectDto.TeacherId && t.IsActive);
    //        if (teacher.FirstOrDefault() == null)
    //            return ResponseDto.Fail("Selected teacher does not exist or is inactive");

    //        // Check if subject code is unique
    //        var subjectRepo = unitOfWork.Repository<User.Api.DbEntities.Subject>();
    //        var existingSubject = await subjectRepo.FindAsync(s => s.SubjectCode == request.SubjectDto.SubjectCode && s.IsActive);
    //        if (existingSubject.Any())
    //            return ResponseDto.Fail("Subject code already exists");

    //        // Map DTO to Entity
    //        var newSubject = new User.Api.DbEntities.Subject
    //        {
    //            SubjectName = request.SubjectDto.SubjectName,
    //            SubjectCode = request.SubjectDto.SubjectCode,
    //            ClassId = request.SubjectDto.ClassId,
    //            TeacherId = request.SubjectDto.TeacherId,
    //            Credits = request.SubjectDto.Credits,
    //            Type = request.SubjectDto.Type,
    //            Description = request.SubjectDto.Description,
    //            CreatedAt = DateTime.UtcNow,
    //            UpdatedAt = DateTime.UtcNow,
    //            IsActive = true
    //        };

    //        await subjectRepo.AddAsync(newSubject);
    //        await unitOfWork.SaveChangesAsync();

    //        return ResponseDto.Success(Data: newSubject.Id, message: "Subject added successfully");
    //    }
    //}
}