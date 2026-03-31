using User.Api.DbEntities;
using User.Api.DbConnection;
using User.Api.Infrastructures;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Repositories
{
    public interface INoticeRepository : IRepository<Notice>
    {
        // Add specific methods if needed
    }

    public class NoticeRepository : Repository<Notice, DumriCollegeDbContext>, INoticeRepository
    {
        public NoticeRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }

    public interface INoticeAttachmentRepository : IRepository<NoticeAttachment>
    {
        // Add specific methods if needed
    }

    public class NoticeAttachmentRepository : Repository<NoticeAttachment, DumriCollegeDbContext>, INoticeAttachmentRepository
    {
        public NoticeAttachmentRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}