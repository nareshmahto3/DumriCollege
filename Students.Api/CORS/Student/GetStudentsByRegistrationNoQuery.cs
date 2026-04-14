using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Student
{
    public class GetStudentsByRegistrationNoQuery : IRequest<ResponseDto>
    {
        public string RegistrationNo { get; }

        public GetStudentsByRegistrationNoQuery(string registrationNo)
        {
            RegistrationNo = registrationNo;
        }
    }
}
