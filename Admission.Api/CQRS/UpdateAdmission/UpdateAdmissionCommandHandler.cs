using Admission.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Admission.Api.CQRS.UpdateAdmission
{

    public class UpdateAdmissionCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateAdmissionCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateAdmissionCommand request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.Repository<Admission.Api.DbEntities.Admission>();
            var existingAdmission= await productRepo.GetByIdAsync(request.AdmissionDto.Id);

            if (existingAdmission == null)
                return ResponseDto.Fail("No record found");

            existingAdmission.Id = request.AdmissionDto.Id;
            existingAdmission.AdmissionId = request.AdmissionDto.AdmissionId;
            existingAdmission.Date = DateTime.Now;           
            productRepo.Update(existingAdmission);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto.Success(existingAdmission);
        }
    }
}


