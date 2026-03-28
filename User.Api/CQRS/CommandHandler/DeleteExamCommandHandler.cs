using User.Api.CQRS.Command;
using User.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace User.Api.CQRS.CommandHandler
{
    public class DeleteExamCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteExamCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var examRepo = unitOfWork.Repository<User.Api.DbEntities.Exam>();
            var exam = await examRepo.GetByIdAsync(request.Id);

            if (exam == null)
                return ResponseDto.Fail("Exam not found");

            // Soft delete - mark as inactive instead of hard delete
            exam.IsActive = false;
            exam.UpdatedAt = DateTime.UtcNow;

            examRepo.Update(exam);
            await unitOfWork.SaveChangesAsync();

            return ResponseDto.Success(null, "Exam deleted successfully");
        }
    }
}
