using LibraryService.Utility.Data.Core.Repositories;
using Master.Api.DbConnection;
using Master.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Master.Api.Infrastructures
{
    public class UserRepository
        : Repository<UserMaster, DumriCollegeDbContext>
    {
        private readonly DumriCollegeDbContext _context;

        public UserRepository(DumriCollegeDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<UserMaster?> GetUserWithRolesAsync(int userId)
        {
            return await _context.UserMasters
                .Include(u => u.UserRoleMappings)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}