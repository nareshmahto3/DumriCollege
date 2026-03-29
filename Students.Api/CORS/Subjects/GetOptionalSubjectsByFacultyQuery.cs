using MediatR;
using Students.Api.DTOs;

namespace Students.Api.CORS.Subjects
{
    public class GetOptionalSubjectsByFacultyQuery : IRequest<ResponseDto>
    {
        public int FacultyId { get; }

        public GetOptionalSubjectsByFacultyQuery(int facultyId)
        {
            FacultyId = facultyId;
        }
    }
}
