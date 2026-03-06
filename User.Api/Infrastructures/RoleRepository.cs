using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{
    public class RoleRepository : Repository<MRole, DumriCommerceCollegeContext>, IRepository<MRole>
    {
        public RoleRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}
