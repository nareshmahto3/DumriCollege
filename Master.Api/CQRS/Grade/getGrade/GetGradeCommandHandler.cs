using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.getGrade
{
    public class GetGradeCommandHandler (IRepository<GradeMaster> _service) : IRequestHandler<GetGradeCommand, PagedResponse<GradeDto>>
    {
        public async Task<PagedResponse<GradeDto>> Handle(GetGradeCommand request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new GradeDto
            {
                GradeId = a.GradeId,
                GradeName = a.GradeName,
                MinMarks = a.MinMarks,
                MaxMarks = a.MaxMarks,
                IsActive = a.IsActive,
                CreatedDate = a.CreatedDate

            });
            return new PagedResponse<GradeDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}

