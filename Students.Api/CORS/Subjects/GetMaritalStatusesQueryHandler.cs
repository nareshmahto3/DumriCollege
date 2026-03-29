using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetMaritalStatusesQueryHandler : IRequestHandler<GetMaritalStatusesQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMaritalStatusesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetMaritalStatusesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<MaritalStatus>();
            var statuses = await repo.GetAllAsync();

            var data = statuses
                .Select(m => new MaritalStatusDto
                {
                    MaritalStatusId = m.MaritalStatusId,
                    MaritalStatusName = m.MaritalStatusName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}

