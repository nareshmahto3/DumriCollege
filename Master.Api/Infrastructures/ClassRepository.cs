using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;

namespace Master.Api.Infrastructures
{
    public class ClassRepository : Repository<ClassMaster, DumriCollegeDbContext>, IRepository<ClassMaster>
    {
        public ClassRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}




