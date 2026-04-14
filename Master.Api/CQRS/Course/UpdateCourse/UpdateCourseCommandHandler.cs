using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.Class.UpdateClass;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.UpdateCourse
{
    public class UpdateCourseCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCourseCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.Repository<CourseMaster>();
            var existingAdmission = await productRepo.GetByIdAsync(request.CourseDto.CourseId);

            if (existingAdmission == null)
                return ResponseDto.Fail("No record found");

            existingAdmission.CourseId = request.CourseDto.CourseId;
            existingAdmission.CourseName = request.CourseDto.CourseName;
            existingAdmission.DurationYears = request.CourseDto.DurationYears;
            existingAdmission.IsActive = request.CourseDto.IsActive;
            existingAdmission.CreatedDate = request.CourseDto.CreatedDate;

            productRepo.Update(existingAdmission);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto.Success(existingAdmission);
        }
    }
}


