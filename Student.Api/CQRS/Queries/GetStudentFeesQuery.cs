using MediatR;
using Student.Api.DTOs;

namespace Student.Api.CQRS.Queries
{
    public class GetStudentFeesQuery : IRequest<ResponseDto>
    {
        public int StudentId { get; }

        public GetStudentFeesQuery(int studentId)
        {
            StudentId = studentId;
        }
    }
}
