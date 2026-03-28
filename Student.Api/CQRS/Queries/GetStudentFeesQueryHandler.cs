using MediatR;
using Student.Api.DTOs;
using Student.Api.Services;

namespace Student.Api.CQRS.Queries
{
    public class GetStudentFeesQueryHandler : IRequestHandler<GetStudentFeesQuery, ResponseDto>
    {
        private readonly IStudentFeeService _studentFeeService;

        public GetStudentFeesQueryHandler(IStudentFeeService studentFeeService)
        {
            _studentFeeService = studentFeeService;
        }

        public async Task<ResponseDto> Handle(GetStudentFeesQuery request, CancellationToken cancellationToken)
        {
            var studentFee = await _studentFeeService.GetStudentFeesAsync(request.StudentId);
            return studentFee is null
                ? ResponseDto.Fail($"Student fees not found for id {request.StudentId}")
                : ResponseDto.Success(studentFee, "Student fee summary loaded successfully.");
        }
    }
}
