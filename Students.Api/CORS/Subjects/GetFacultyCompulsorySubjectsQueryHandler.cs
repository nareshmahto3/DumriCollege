using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetFacultyCompulsorySubjectsQueryHandler
        : IRequestHandler<GetFacultyCompulsorySubjectsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacultyCompulsorySubjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetFacultyCompulsorySubjectsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<FacultyCompulsorySubject>();
            var all = await repo.GetAllAsync();

            var data = all
                .Where(x => x.FacultyId == request.FacultyId)
                .Select(x => new FacultyCompulsorySubjectDto
                {
                    Id = x.Id,
                    FacultyId = x.FacultyId,
                    SubjectName = x.SubjectName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}