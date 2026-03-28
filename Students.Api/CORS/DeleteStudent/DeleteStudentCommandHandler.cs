//using LibraryService.Utility.Data.Core.Interfaces;
//using MediatR;
//using Students.Api.DbEntities;
//using Students.Api.DTOs;

//namespace Students.Api.CORS.DeleteStudent
//{
//    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ResponseDto>
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<ResponseDto> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
//        {
//            var appRepo = _unitOfWork.Repository<StudentApplication>();
//            var issueRepo = _unitOfWork.Repository<StudentApplicationIssue>();

//            // Find application
//            var application = await appRepo.GetByIdAsync(request.ApplicationId);
//            if (application == null)
//                return ResponseDto.Fail("Application not found");

//            // Soft delete: record issue / flag instead of physical delete
//            var issue = new StudentApplicationIssue
//            {
//                ApplicationId = application.ApplicationId,
//                FieldName = "Application",
//                Comment = "Soft deleted by user.",
//                Status = "Deleted",
//                CreatedDate = DateTime.Now
//            };

//            await issueRepo.AddAsync(issue);

//            await _unitOfWork.SaveChangesAsync();

//            return ResponseDto.Success(null, "Student application marked as deleted (soft delete)");
//        }
//    }
//}
