using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Student
{
    public class GetAllStudentsQuery : IRequest<ResponseDto>
    {
    }
}
