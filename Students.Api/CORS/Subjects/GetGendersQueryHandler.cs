using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGendersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Gender>();
            var genders = await repo.GetAllAsync();

            var data = genders
                .Select(g => new GenderDto
                {
                    GenderId = g.GenderId,
                    GenderName = g.GenderName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
