using Admission.Api.DTOs;

using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Admission.Api.CQRS.AddAdmission
{
    public class AddAdmissionCommandHandler
    {
    }
    public class AddProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddAdmissionCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddAdmissionCommand request, CancellationToken cancellationToken)
        {

            if (request.AdmissionDto == null)
                return ResponseDto.Fail("Admission data is null");

           // var product = _mapper.Map<Admission.Api.DbEntities.Admission>(request.AdmissionDto);
           var admission=new Admission.Api.DbEntities.Admission
            {
               AdmissionId = request.AdmissionDto.AdmissionId,
               Date = request.AdmissionDto.Date
               // StudentName = request.AdmissionDto.StudentName,
               //  Age = request.AdmissionDto.Age,
               //  Course = request.AdmissionDto.Course,
               // AdmissionDate = request.AdmissionDto.AdmissionDate
           };
            var productRepo = unitOfWork.Repository<Admission.Api.DbEntities.Admission>();
            await productRepo.AddAsync(admission);
            await unitOfWork.SaveChangesAsync();


            return ResponseDto.Success(Data: null, message: "Admission added successfully");
        }
    }
}
