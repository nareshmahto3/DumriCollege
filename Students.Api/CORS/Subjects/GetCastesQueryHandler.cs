using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetCastesQueryHandler : IRequestHandler<GetCastesQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCastesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetCastesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Caste>();
            var castes = await repo.GetAllAsync();

            var data = castes
                .Select(c => new CasteDto
                {
                    CasteId = c.CasteId,
                    CasteName = c.CasteName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
