using User.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Infrastructures
{
    public class ExamRepository : Repository<User.Api.DbEntities.Exam, DumriCommerceCollegeContext>, IRepository<User.Api.DbEntities.Exam>
    {
        public ExamRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }

    public class ExamSubjectRepository : Repository<User.Api.DbEntities.ExamSubject, DumriCommerceCollegeContext>, IRepository<User.Api.DbEntities.ExamSubject>
    {
        public ExamSubjectRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}
