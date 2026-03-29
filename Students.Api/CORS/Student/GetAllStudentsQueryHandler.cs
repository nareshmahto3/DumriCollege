using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Students.Api.CORS.Student
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var genderRepo = _unitOfWork.Repository<Gender>();

            var applications = await appRepo.GetAllAsync();
            var genders = await genderRepo.GetAllAsync();

            var genderLookup = genders.ToDictionary(g => g.GenderId, g => g.GenderName);

            var list = applications
                .Select(a => new StudentListDto
                {
                    ApplicationId  = a.ApplicationId,
                    StudentName    = a.StudentName    ?? string.Empty,
                    RegistrationNo = a.RegistrationNo ?? string.Empty,
                    MobileNumber   = a.MobileNumber   ?? string.Empty,
                    StudentId      = null,
                    Gender         = a.GenderId.HasValue && genderLookup.TryGetValue(a.GenderId.Value, out var gName)
                                        ? gName
                                        : string.Empty,
                    Status         = "Pending",
                    CreatedDate    = a.CreatedDate
                })
                .ToList();

            return ResponseDto.Success(list);
        }
    }
}
