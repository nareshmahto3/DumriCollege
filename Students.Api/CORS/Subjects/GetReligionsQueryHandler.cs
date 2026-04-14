using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetReligionsQueryHandler : IRequestHandler<GetReligionsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReligionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetReligionsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Religion>();
            var religions = await repo.GetAllAsync();

            var data = religions
                .Select(r => new ReligionDto
                {
                    ReligionId = r.ReligionId,
                    ReligionName = r.ReligionName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
