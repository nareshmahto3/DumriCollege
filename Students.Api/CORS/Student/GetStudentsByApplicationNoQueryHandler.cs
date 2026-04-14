using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Students.Api.CORS.Student
{
    public class GetStudentsByApplicationNoQueryHandler : IRequestHandler<GetStudentsByApplicationNoQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentsByApplicationNoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetStudentsByApplicationNoQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ApplicationNo))
                return ResponseDto.Fail("ApplicationNo is required.");

            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var genderRepo = _unitOfWork.Repository<Gender>();
            var classRepo = _unitOfWork.Repository<ClassMaster>();

            var applications = (await appRepo.GetAllAsync())
                .Where(a => a.IsActive)
                .ToList();
            var genders = await genderRepo.GetAllAsync();
            var classes = await classRepo.GetAllAsync();

            var genderLookup = genders.ToDictionary(g => g.GenderId, g => g.GenderName ?? string.Empty);
            var classLookup = classes.ToDictionary(c => c.Id, c => c.Name ?? string.Empty);

            var filterValue = request.ApplicationNo.Trim();

            var list = applications
                .Where(a => !string.IsNullOrWhiteSpace(a.ApplicationNo) &&
                            a.ApplicationNo.Contains(filterValue, System.StringComparison.OrdinalIgnoreCase))
                .Select(a => new StudentListDto
                {
                    ApplicationId = a.ApplicationId,
                    ApplicationNo = a.ApplicationNo ?? string.Empty,
                    RegistrationNo = a.RegistrationNo ?? string.Empty,
                    StudentName = a.StudentName ?? string.Empty,
                    ClassName = a.ClassId.HasValue && classLookup.TryGetValue(a.ClassId.Value, out var className)
                                    ? className
                                    : string.Empty,
                    ApplicationStatus = a.ApplicationStatus ?? "Pending",
                    Gender = a.GenderId.HasValue && genderLookup.TryGetValue(a.GenderId.Value, out var gName)
                                ? gName
                                : string.Empty,
                    MobileNumber = a.MobileNumber ?? string.Empty,
                    StudentId = null,
                    Status = a.ApplicationStatus ?? "Pending",
                    CreatedDate = a.CreatedDate
                })
                .ToList();

            return ResponseDto.Success(list);
        }
    }
}
