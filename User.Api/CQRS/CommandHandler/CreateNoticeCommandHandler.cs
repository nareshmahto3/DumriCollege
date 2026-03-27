using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Api.CQRS.Command;
using User.Api.DbEntities;
using User.Api.DTOs;
using User.Api.Infrastructures;

namespace User.Api.CQRS.CommandHandler
{
    public class CreateNoticeCommandHandler : IRequestHandler<CreateNoticeCommand, NoticeResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNoticeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NoticeResponseDto> Handle(CreateNoticeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notice = new Notice
                {
                    Title = request.Notice.Title,
                    Category = request.Notice.Category,
                    Priority = request.Notice.Priority,
                    TargetAudience = request.Notice.TargetAudience,
                    PublishDate = request.Notice.PublishDate,
                    ExpiryDate = request.Notice.ExpiryDate,
                    Content = request.Notice.Content,
                    NoticeNumber = request.Notice.NoticeNumber,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Repository<Notice>().AddAsync(notice);
                await _unitOfWork.SaveChangesAsync();

                // Handle file uploads
                if (request.Notice.Attachments != null && request.Notice.Attachments.Any())
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "notices");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    foreach (var file in request.Notice.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var attachment = new NoticeAttachment
                            {
                                NoticeId = notice.Id,
                                FileName = file.FileName,
                                FilePath = $"/uploads/notices/{fileName}",
                                IsActive = true
                            };

                            await _unitOfWork.Repository<NoticeAttachment>().AddAsync(attachment);
                        }
                    }

                    await _unitOfWork.SaveChangesAsync();
                }

                var noticeDto = await GetNoticeDto(notice.Id);
                return new NoticeResponseDto
                {
                    Success = true,
                    Message = "Notice created successfully",
                    Data = noticeDto
                };
            }
            catch (Exception ex)
            {
                return new NoticeResponseDto
                {
                    Success = false,
                    Message = $"Error creating notice: {ex.Message}"
                };
            }
        }

        private async Task<NoticeDto> GetNoticeDto(int noticeId)
        {
            var notice = await _unitOfWork.Repository<Notice>().GetByIdAsync(noticeId);
            var attachments = await _unitOfWork.Repository<NoticeAttachment>().FindAsync(na => na.NoticeId == noticeId && na.IsActive);

            return new NoticeDto
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
        }
    }

    public class UpdateNoticeCommandHandler : IRequestHandler<UpdateNoticeCommand, NoticeResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNoticeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NoticeResponseDto> Handle(UpdateNoticeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notice = await _unitOfWork.Repository<Notice>().GetByIdAsync(request.Notice.Id);
                if (notice == null)
                {
                    return new NoticeResponseDto
                    {
                        Success = false,
                        Message = "Notice not found"
                    };
                }

                notice.Title = request.Notice.Title;
                notice.Category = request.Notice.Category;
                notice.Priority = request.Notice.Priority;
                notice.TargetAudience = request.Notice.TargetAudience;
                notice.PublishDate = request.Notice.PublishDate;
                notice.ExpiryDate = request.Notice.ExpiryDate;
                notice.Content = request.Notice.Content;
                notice.NoticeNumber = request.Notice.NoticeNumber;
                notice.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Notice>().Update(notice);

                // Handle new file uploads
                if (request.Notice.Attachments != null && request.Notice.Attachments.Any())
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "notices");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    foreach (var file in request.Notice.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var attachment = new NoticeAttachment
                            {
                                NoticeId = notice.Id,
                                FileName = file.FileName,
                                FilePath = $"/uploads/notices/{fileName}",
                                IsActive = true
                            };

                            await _unitOfWork.Repository<NoticeAttachment>().AddAsync(attachment);
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                var noticeDto = await GetNoticeDto(notice.Id);
                return new NoticeResponseDto
                {
                    Success = true,
                    Message = "Notice updated successfully",
                    Data = noticeDto
                };
            }
            catch (Exception ex)
            {
                return new NoticeResponseDto
                {
                    Success = false,
                    Message = $"Error updating notice: {ex.Message}"
                };
            }
        }

        private async Task<NoticeDto> GetNoticeDto(int noticeId)
        {
            var notice = await _unitOfWork.Repository<Notice>().GetByIdAsync(noticeId);
            var attachments = await _unitOfWork.Repository<NoticeAttachment>().FindAsync(na => na.NoticeId == noticeId && na.IsActive);

            return new NoticeDto
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
        }
    }

    public class DeleteNoticeCommandHandler : IRequestHandler<DeleteNoticeCommand, NoticeResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNoticeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NoticeResponseDto> Handle(DeleteNoticeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notice = await _unitOfWork.Repository<Notice>().GetByIdAsync(request.Id);
                if (notice == null)
                {
                    return new NoticeResponseDto
                    {
                        Success = false,
                        Message = "Notice not found"
                    };
                }

                notice.IsActive = false;
                notice.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Notice>().Update(notice);
                await _unitOfWork.SaveChangesAsync();

                return new NoticeResponseDto
                {
                    Success = true,
                    Message = "Notice deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new NoticeResponseDto
                {
                    Success = false,
                    Message = $"Error deleting notice: {ex.Message}"
                };
            }
        }
    }
}