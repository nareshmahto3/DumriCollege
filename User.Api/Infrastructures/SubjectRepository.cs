using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{
    public class SubjectRepository : Repository<Subject, DumriCollegeDbContext>, IRepository<Subject>
    {
        public SubjectRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}