using User.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Infrastructures
{
    public class ExamRepository : Repository<User.Api.DbEntities.Exam, DumriCollegeDbContext>, IRepository<User.Api.DbEntities.Exam>
    {
        public ExamRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }

    public class ExamSubjectRepository : Repository<User.Api.DbEntities.ExamSubject, DumriCollegeDbContext>, IRepository<User.Api.DbEntities.ExamSubject>
    {
        public ExamSubjectRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}
