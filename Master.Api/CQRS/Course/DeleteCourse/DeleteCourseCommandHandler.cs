using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.DeleteClass;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Course.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<CourseMaster>();
            var existing = await repo.GetByIdAsync(request.CourseId);

            if (existing == null)
                return ResponseDto.Fail("No record found");

            repo.Remove(existing);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: null, message: "Record deleted successfully");
        }
    }
}