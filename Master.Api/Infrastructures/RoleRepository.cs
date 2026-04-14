using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Master.Api.Infrastructures
{
    public class RoleRepository
        : Repository<RoleMaster, DumriCollegeDbContext>
    {
        private readonly DumriCollegeDbContext _context;

        public RoleRepository(DumriCollegeDbContext context)
            : base(context)
        {
            _context = context;
        }

        // Custom method example
        public async Task<RoleMaster?> GetRoleWithUsersAsync(int roleId)
        {
            return await _context.RoleMasters
                .Include(r => r.UserRoleMappings)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
    }
}