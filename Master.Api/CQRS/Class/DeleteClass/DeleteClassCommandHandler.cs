using LibraryService.Utility.Data.Core.Interfaces;
using Master.Api.CQRS.DeleteClass;
using Master.Api.DbEntities;
using Master.Api.DTOs;
using MediatR;


namespace Master.Api.CQRS.DeleteClass
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<ClassMaster>();
            var existing = await repo.GetByIdAsync(request.ClassId);

            if (existing == null)
                return ResponseDto.Fail("No record found");

            repo.Remove(existing);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(Data: null, message: "Record deleted successfully");
        }
    }
}