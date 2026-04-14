using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.Course.DeleteCourse;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;

namespace Master.Api.CQRS.Grade.DeleteGrade
{
    public class DeleteGradeCommandhandler : IRequestHandler<DeleteGradeCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGradeCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<GradeMaster>();
            var existing = await repo.GetByIdAsync(request.GradeId);

            if (existing == null)
                return ResponseDto.Fail("No record found");

            repo.Remove(existing);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: null, message: "Record deleted successfully");
        }
    }
}