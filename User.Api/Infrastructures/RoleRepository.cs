using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using User.Api.DbConnection;
using User.Api.DbEntities;

namespace User.Api.Infrastructures
{
    public class RoleRepository : Repository<MRole, DumriCollegeDbContext>, IRepository<MRole>
    {
        public RoleRepository(DumriCollegeDbContext context) : base(context)
        {
        }
    }
}
