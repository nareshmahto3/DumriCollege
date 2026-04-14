using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.Course.UpdateCourse;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.UpdateGrade
{
    public class UpdateGradeCommandHnadler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateGradeCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.Repository<GradeMaster>();
            var existingAdmission = await productRepo.GetByIdAsync(request.GradeDto.GradeId);

            if (existingAdmission == null)
                return ResponseDto.Fail("No record found");

            existingAdmission.GradeId = request.GradeDto.GradeId;
            existingAdmission.GradeName = request.GradeDto.GradeName;
            existingAdmission.MinMarks = request.GradeDto.MinMarks;
            existingAdmission.MaxMarks = request.GradeDto.MaxMarks;
            existingAdmission.IsActive = request.GradeDto.IsActive;
            existingAdmission.CreatedDate = request.GradeDto.CreatedDate;



            productRepo.Update(existingAdmission);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto.Success(existingAdmission);
        }
    }
}


