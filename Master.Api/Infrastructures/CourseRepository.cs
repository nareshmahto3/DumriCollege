using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;

namespace Master.Api.Infrastructures
{
    public class CourseRepository : Repository<CourseMaster, DumriCollegeDbContext>, IRepository<CourseMaster>
    {
        public CourseRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}


