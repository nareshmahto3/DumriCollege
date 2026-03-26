using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.Course.AddCourse;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.AddGrade
{
    public class AddGradeCommandHandler
    {
    }
    public class AddProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddGradeCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {

            if (request.GradeDto == null)
                return ResponseDto.Fail("Admission data is null");

            // var product = _mapper.Map<Admission.Api.DbEntities.Admission>(request.AdmissionDto);
            var admission = new GradeMaster
            {
                GradeId = request.GradeDto.GradeId,
                GradeName = request.GradeDto.GradeName,
                MinMarks = request.GradeDto.MinMarks,
                MaxMarks = request.GradeDto.MaxMarks,
                IsActive = request.GradeDto.IsActive,
                CreatedDate = request.GradeDto.CreatedDate,

                // StudentName = request.AdmissionDto.StudentName,
                //  Age = request.AdmissionDto.Age,
                //  Course = request.AdmissionDto.Course,
                // AdmissionDate = request.AdmissionDto.AdmissionDate
            };
            var productRepo = unitOfWork.Repository<GradeMaster>();
            await productRepo.AddAsync(admission);
            await unitOfWork.SaveChangesAsync();


            return ResponseDto.Success(Data: null, message: "Admission added successfully");
        }
    }
}

