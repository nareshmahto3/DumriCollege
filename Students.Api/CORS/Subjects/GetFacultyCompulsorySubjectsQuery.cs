using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetFacultyCompulsorySubjectsQuery : IRequest<ResponseDto>
    {
        public int FacultyId { get; }

        public GetFacultyCompulsorySubjectsQuery(int facultyId)
        {
            FacultyId = facultyId;
        }
    }
}
