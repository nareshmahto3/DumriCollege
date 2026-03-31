using User.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Infrastructures
{
    public class StudentDocumentRepository : Repository<User.Api.DbEntities.StudentDocument, DumriCollegeDbContext>, IRepository<User.Api.DbEntities.StudentDocument>
    {
        public StudentDocumentRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}
