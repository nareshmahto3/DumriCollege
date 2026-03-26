using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Student
{
    public class GetStudentByIdQuery : IRequest<ResponseDto>
    {
        public int ApplicationId { get; }

        public GetStudentByIdQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
