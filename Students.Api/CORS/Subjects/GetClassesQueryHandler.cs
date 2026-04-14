using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Students.Api.DbEntities;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetClassesQueryHandler : IRequestHandler<GetClassesQuery, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClassesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(GetClassesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<ClassMaster>();
            var classes = await repo.GetAllAsync();

            var data = classes
                .Select(c => new ClassMasterDto
                {
                    Id = c.Id,
                    Name = c.Name ?? string.Empty
                })
                .ToList();

            return ResponseDto.Success(data);
        }
    }
}
