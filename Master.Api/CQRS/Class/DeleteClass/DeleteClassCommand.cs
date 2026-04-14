using Master.Api.DTOs;
using MediatR;


namespace Master.Api.CQRS.DeleteClass

{
    public class DeleteClassCommand : IRequest<ResponseDto>
    {
        public int ClassId { get; set; }
    }
}


