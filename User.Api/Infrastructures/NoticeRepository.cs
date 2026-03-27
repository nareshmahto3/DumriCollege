using User.Api.DbEntities;
using User.Api.Infrastructures;

namespace User.Api.Repositories
{
    public interface INoticeRepository : IRepository<Notice>
    {
        // Add specific methods if needed
    }

    public class NoticeRepository : Repository<Notice>, INoticeRepository
    {
        public NoticeRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }

    public interface INoticeAttachmentRepository : IRepository<NoticeAttachment>
    {
        // Add specific methods if needed
    }

    public class NoticeAttachmentRepository : Repository<NoticeAttachment>, INoticeAttachmentRepository
    {
        public NoticeAttachmentRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}