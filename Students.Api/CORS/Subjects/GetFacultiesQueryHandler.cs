using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetFacultiesQueryHandler
        : IRequestHandler<GetFacultiesQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacultiesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetFacultiesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Faculty>();
            var faculties = await repo.GetAllAsync();

            var data = faculties
                .Select(f => new FacultyDto
                {
                    FacultyId = f.FacultyId,
                    FacultyName = f.FacultyName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}