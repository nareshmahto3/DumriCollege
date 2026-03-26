using Master.Api.DbConnection;
using Master.Api.DbEntities;

namespace Master.Api.CQRS.Users.UpdateUser
{
    public class UpdateUserCommandHandler
    {
        private readonly DumriCollegeDbContext _context;

        public UpdateUserCommandHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand command)
        {
            // 1️⃣ Get user
            var user = await _context.UserMasters.FindAsync(command.UserId);
            if (user == null)
                throw new Exception("User not found");

            // 2️⃣ Update user fields
            user.UserName = command.UserName;
            user.Email = command.Email;
            if (!string.IsNullOrEmpty(command.PasswordHash))
                user.PasswordHash = command.PasswordHash;

            // 3️⃣ Update role mappings
            var existingRoles = _context.UserRoleMappings.Where(r => r.UserId == command.UserId);
            _context.UserRoleMappings.RemoveRange(existingRoles);

            foreach (var roleId in command.RoleIds)
            {
                _context.UserRoleMappings.Add(new UserRoleMapping
                {
                    UserId = command.UserId,
                    RoleId = roleId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
