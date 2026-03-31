using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{  

    public class ClassRepository : Repository<User.Api.DbEntities.Class, DumriCollegeDbContext>, IRepository<Class>
    {
        public ClassRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}