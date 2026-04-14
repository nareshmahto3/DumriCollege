//using Employee.Api.DTOs;
//using LibraryService.Utility.Data.Core.Interfaces;
//using MediatR;

//namespace Employee.Api.CQRS.DeleteEmployee
//{
//    public class DeleteEmployeeCommandHandler
//        : IRequestHandler<DeleteEmployeeCommand, ResponseDto>
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<ResponseDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
//        {
//            var repo = _unitOfWork.Repository<Employee.Api.DbEntities.Employee>();

//            var existing = await repo.GetByIdAsync(request.EmployeeId);

//            if (existing == null)
//                return ResponseDto.Fail("No record found");

//            repo.Remove(existing);

//            // ✅ Pass cancellationToken here
//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return ResponseDto.Success(null, "Record deleted successfully");
//        }
//    }
//}
