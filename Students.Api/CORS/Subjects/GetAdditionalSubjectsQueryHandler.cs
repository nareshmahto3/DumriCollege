using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetAdditionalSubjectsQueryHandler
        : IRequestHandler<GetAdditionalSubjectsQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAdditionalSubjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetAdditionalSubjectsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<AdditionalSubject>();
            var all = await repo.GetAllAsync();
            var data = all
                .Select(a => new AdditionalSubjectDto
                {
                    AdditionalSubjectId = a.AdditionalSubjectId,
                    SubjectName = a.SubjectName
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
