using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Class.GetClass
{
    public class GetClassCommandHandler(IRepository<ClassMaster> _service) : IRequestHandler<GetClassCommand, PagedResponse<ClassDto>>
    {
        public async Task<PagedResponse<ClassDto>> Handle(GetClassCommand request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new ClassDto
            {
                ClassId = a.ClassId,
                CourseId = a.CourseId,
                ClassName = a.ClassName,
                IsActive = a.IsActive,
                CreatedDate = a.CreatedDate
            });
            return new PagedResponse<ClassDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}




