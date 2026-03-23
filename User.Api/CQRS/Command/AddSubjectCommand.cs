using User.Api.DTOs;
using MediatR;

namespace User.Api.CQRS.Command
{
    public class AddSubjectCommand : IRequest<ResponseDto>
    {
        public AddSubjectDto SubjectDto { get; set; }

        public AddSubjectCommand(AddSubjectDto subjectDto)
        {
            SubjectDto = subjectDto;
        }
    }
}