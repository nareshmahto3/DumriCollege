using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Api.CQRS.Command;
using User.Api.DbEntities;
using User.Api.DTOs;
using User.Api.Infrastructures;
using LibraryService.Utility.Data.Core.Interfaces;

namespace User.Api.CQRS.CommandHandler
{
    public class NoticeQueryHandlers :
        IRequestHandler<GetNoticeByIdQuery, NoticeResponseDto>,
        IRequestHandler<GetAllNoticesQuery, NoticeListResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoticeQueryHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NoticeResponseDto> Handle(GetNoticeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var notice = await _unitOfWork.Repository<Notice>().GetByIdAsync(request.Id);
                if (notice == null || !notice.IsActive)
                {
                    return new NoticeResponseDto
                    {
                        Success = false,
                        Message = "Notice not found"
                    };
                }

                var attachments = await _unitOfWork.Repository<NoticeAttachment>().FindAsync(na => na.NoticeId == request.Id && na.IsActive);

                var noticeDto = new NoticeDto
                {
                    Id = notice.Id,
                    Title = notice.Title,
                    Category = notice.Category,
                    Priority = notice.Priority,
                    TargetAudience = notice.TargetAudience,
                    PublishDate = notice.PublishDate,
                    ExpiryDate = notice.ExpiryDate,
                    Content = notice.Content,
                    NoticeNumber = notice.NoticeNumber,
                    IsActive = notice.IsActive,
                    CreatedAt = notice.CreatedAt,
                    UpdatedAt = notice.UpdatedAt,
                    Attachments = attachments.Select(a => new NoticeAttachmentDto
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        FilePath = a.FilePath
                    }).ToList()
                };

                return new NoticeResponseDto
                {
                    Success = true,
                    Message = "Notice retrieved successfully",
                    Data = noticeDto
                };
            }
            catch (Exception ex)
            {
                return new NoticeResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving notice: {ex.Message}"
                };
            }
        }

        public async Task<NoticeListResponseDto> Handle(GetAllNoticesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allNotices = await _unitOfWork.Repository<Notice>().GetAllAsync();
                var query = allNotices.Where(n => n.IsActive).AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(n => n.Title.Contains(request.SearchTerm) || n.Content.Contains(request.SearchTerm));
                }

                if (!string.IsNullOrEmpty(request.Category))
                {
                    query = query.Where(n => n.Category == request.Category);
                }

                if (!string.IsNullOrEmpty(request.Priority))
                {
                    query = query.Where(n => n.Priority == request.Priority);
                }

                var totalCount = await query.CountAsync();
                var notices = await query
                    .OrderByDescending(n => n.CreatedAt)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var noticeDtos = new List<NoticeDto>();
                foreach (var notice in notices)
                {
                    var attachments = await _unitOfWork.Repository<NoticeAttachment>().FindAsync(na => na.NoticeId == notice.Id && na.IsActive);

                    noticeDtos.Add(new NoticeDto
                    {
                        Id = notice.Id,
                        Title = notice.Title,
                        Category = notice.Category,
                        Priority = notice.Priority,
                        TargetAudience = notice.TargetAudience,
                        PublishDate = notice.PublishDate,
                        ExpiryDate = notice.ExpiryDate,
                        Content = notice.Content,
                        NoticeNumber = notice.NoticeNumber,
                        IsActive = notice.IsActive,
                        CreatedAt = notice.CreatedAt,
                        UpdatedAt = notice.UpdatedAt,
                        Attachments = attachments.Select(a => new NoticeAttachmentDto
                        {
                            Id = a.Id,
                            FileName = a.FileName,
                            FilePath = a.FilePath
                        }).ToList()
                    });
                }

                var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

                return new NoticeListResponseDto
                {
                    Success = true,
                    Message = "Notices retrieved successfully",
                    Data = noticeDtos,
                    MetaData = new PaginationMetaData
                    {
                        CurrentPage = request.PageNumber,
                        TotalPages = totalPages,
                        PageSize = request.PageSize,
                        TotalCount = totalCount
                    }
                };
            }
            catch (Exception ex)
            {
                return new NoticeListResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving notices: {ex.Message}"
                };
            }
        }
    }
}