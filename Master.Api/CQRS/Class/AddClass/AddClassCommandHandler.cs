using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.Course.AddCourse;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.AddClass
{
    public class AddClassCommandHandler
    {
    }
    public class AddProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddClassCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddClassCommand request, CancellationToken cancellationToken)
        {

            if (request.ClassDto == null)
                return ResponseDto.Fail("Admission data is null");

            // var product = _mapper.Map<Admission.Api.DbEntities.Admission>(request.AdmissionDto);
            var admission = new ClassMaster
            {
                ClassId = request.ClassDto.ClassId,
                    CourseId = request.ClassDto.CourseId,
                    ClassName = request.ClassDto.ClassName,
                    IsActive = request.ClassDto.IsActive,
                    CreatedDate = DateTime.UtcNow

                // StudentName = request.AdmissionDto.StudentName,
                //  Age = request.AdmissionDto.Age,
                //  Course = request.AdmissionDto.Course,
                // AdmissionDate = request.AdmissionDto.AdmissionDate
            };
            var productRepo = unitOfWork.Repository<ClassMaster>();
            await productRepo.AddAsync(admission);
            await unitOfWork.SaveChangesAsync();


            return ResponseDto.Success(Data: null, message: "Admission added successfully");
        }
    }
}

