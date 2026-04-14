using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.UpdateClass
{
    public class UpdateClassCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateClassCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.Repository<ClassMaster>();
            var existingAdmission = await productRepo.GetByIdAsync(request.ClassDto.ClassId);

            if (existingAdmission == null)
                return ResponseDto.Fail("No record found");

            existingAdmission.ClassId = request.ClassDto.ClassId;
            existingAdmission.CourseId = request.ClassDto.CourseId;
            existingAdmission.ClassName = request.ClassDto.ClassName;
            existingAdmission.IsActive = request.ClassDto.IsActive;
            existingAdmission.CreatedDate = request.ClassDto.CreatedDate;


            productRepo.Update(existingAdmission);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto.Success(existingAdmission);
        }
    }
}


