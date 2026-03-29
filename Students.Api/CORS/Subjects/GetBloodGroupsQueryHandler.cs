using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetBloodGroupsQueryHandler : IRequestHandler<GetBloodGroupsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBloodGroupsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetBloodGroupsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<BloodGroup>();
            var bloodGroups = await repo.GetAllAsync();

            var data = bloodGroups
                .Select(b => new BloodGroupDto
                {
                    BloodGroupId = b.BloodGroupId,
                    BloodGroupName = b.BloodGroupName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}