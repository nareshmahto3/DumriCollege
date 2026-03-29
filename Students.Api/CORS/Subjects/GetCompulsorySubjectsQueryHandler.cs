using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetCompulsorySubjectsQueryHandler
          : IRequestHandler<GetCompulsorySubjectsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompulsorySubjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetCompulsorySubjectsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<CompulsorySubject>();
            var all = await repo.GetAllAsync();
            var data = all
                .Select(c => new CompulsorySubjectDto
                {
                    CompulsorySubjectId = c.CompulsorySubjectId,
                    SubjectName = c.SubjectName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
