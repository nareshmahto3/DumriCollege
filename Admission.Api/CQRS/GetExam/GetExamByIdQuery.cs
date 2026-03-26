using Admission.Api.DTOs;
using MediatR;

namespace Admission.Api.CQRS.GetExam;

public class GetExamByIdQuery : IRequest<ResponseDto>
{
    public int Id { get; set; }
    public GetExamByIdQuery(int id) => Id = id;
}
