using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;


namespace Master.Api.CQRS.Course.AddCourse
{
    public class AddCourseCommandHandler
    {
    }


    public class AddProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCourseCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {

            if (request.CourseDto == null)
                return ResponseDto.Fail("Admission data is null");

            // var product = _mapper.Map<Admission.Api.DbEntities.Admission>(request.AdmissionDto);
            var admission = new CourseMaster
            {
                CourseId = request.CourseDto.CourseId,
                CourseName = request.CourseDto.CourseName,
                DurationYears = request.CourseDto.DurationYears,
                IsActive = request.CourseDto.IsActive,
                CreatedDate = request.CourseDto.CreatedDate,

                // StudentName = request.AdmissionDto.StudentName,
                //  Age = request.AdmissionDto.Age,
                //  Course = request.AdmissionDto.Course,
                // AdmissionDate = request.AdmissionDto.AdmissionDate
            };
            var productRepo = unitOfWork.Repository<CourseMaster>();
            await productRepo.AddAsync(admission);
            await unitOfWork.SaveChangesAsync();


            return ResponseDto.Success(Data: null, message: "Admission added successfully");
        }
    }
}