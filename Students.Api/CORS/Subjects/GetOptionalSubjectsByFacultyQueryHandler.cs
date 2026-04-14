using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetOptionalSubjectsByFacultyQueryHandler
       : IRequestHandler<GetOptionalSubjectsByFacultyQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOptionalSubjectsByFacultyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetOptionalSubjectsByFacultyQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<OptionalSubject>();
            var all = await repo.GetAllAsync();
            var filtered = all
                .Where(o => o.FacultyId == request.FacultyId)
                .Select(o => new OptionalSubjectDto
                {
                    OptionalSubjectId = o.OptionalSubjectId,
                    FacultyId = o.FacultyId,
                    SubjectName = o.SubjectName
                })
                .ToList();

            return ResponseDto.Success(filtered);
        }
    }
}