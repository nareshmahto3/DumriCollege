using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Master.Api.Infrastructures
{
    public class UserRoleMappingRepository
        : Repository<UserRoleMapping, DumriCollegeDbContext>
    {
        private readonly DumriCollegeDbContext _context;

        public UserRoleMappingRepository(DumriCollegeDbContext context)
            : base(context)
        {
            _context = context;
        }

        // Custom method example
        public async Task<IEnumerable<UserRoleMapping>> GetMappingsByUserIdAsync(int userId)
        {
            return await _context.UserRoleMappings
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }
    }
}