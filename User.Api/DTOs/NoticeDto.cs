using System.ComponentModel.DataAnnotations;

namespace User.Api.DTOs
{
    public class NoticeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? NoticeNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<NoticeAttachmentDto> Attachments { get; set; } = new List<NoticeAttachmentDto>();
    }

    public class NoticeAttachmentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
    }

    public class CreateNoticeDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Priority { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TargetAudience { get; set; } = string.Empty;

        [Required]
        public DateTime PublishDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(50)]
        public string? NoticeNumber { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }

    public class UpdateNoticeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Priority { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TargetAudience { get; set; } = string.Empty;

        [Required]
        public DateTime PublishDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(50)]
        public string? NoticeNumber { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }

    public class NoticeResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public NoticeDto? Data { get; set; }
    }

    public class NoticeListResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<NoticeDto> Data { get; set; } = new List<NoticeDto>();
        public PaginationMetaData? MetaData { get; set; }
    }

    public class PaginationMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}