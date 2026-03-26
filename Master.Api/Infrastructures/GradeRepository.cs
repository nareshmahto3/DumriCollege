using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;

namespace Master.Api.Infrastructures
{
    public class GradeRepository : Repository<GradeMaster, DumriCollegeDbContext>, IRepository<GradeMaster>
    {
        public GradeRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}


