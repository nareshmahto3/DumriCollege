using MediatR;
using User.Api.DTOs;

namespace User.Api.CQRS.Command
{
    public record CreateNoticeCommand(CreateNoticeDto Notice) : IRequest<NoticeResponseDto>;
    public record UpdateNoticeCommand(UpdateNoticeDto Notice) : IRequest<NoticeResponseDto>;
    public record DeleteNoticeCommand(int Id) : IRequest<NoticeResponseDto>;

    public record GetNoticeByIdQuery(int Id) : IRequest<NoticeResponseDto>;
    public record GetAllNoticesQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null, string? Category = null, string? Priority = null) : IRequest<NoticeListResponseDto>;
}