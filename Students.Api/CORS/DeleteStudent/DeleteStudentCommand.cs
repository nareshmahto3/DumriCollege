using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<ResponseDto>
    {
        public int ApplicationId { get; }

        public DeleteStudentCommand(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
