using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Student
{
    public class GetStudentsByApplicationNoQuery : IRequest<ResponseDto>
    {
        public string ApplicationNo { get; }

        public GetStudentsByApplicationNoQuery(string applicationNo)
        {
            ApplicationNo = applicationNo;
        }
    }
}
