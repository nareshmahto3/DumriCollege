using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{
    public class ClassRepository : Repository<Class, DumriCommerceCollegeContext>, IRepository<Class>
    {
        public ClassRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}