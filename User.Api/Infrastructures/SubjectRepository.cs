using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{
    public class SubjectRepository : Repository<Subject, DumriCommerceCollegeContext>, IRepository<Subject>
    {
        public SubjectRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}