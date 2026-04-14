using Master.Api.DbConnection;

namespace Master.Api.CQRS.Users.DeleteUser
{
    public class DeleteUserCommandHandler
    {
        private readonly DumriCollegeDbContext _context;

        public DeleteUserCommandHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserCommand command)
        {
            // 1️⃣ Find user
            var user = await _context.UserMasters.FindAsync(command.UserId);
            if (user == null)
                throw new Exception("User not found");

            // 2️⃣ Remove role mappings
            var roleMappings = _context.UserRoleMappings.Where(r => r.UserId == command.UserId);
            _context.UserRoleMappings.RemoveRange(roleMappings);

            // 3️⃣ Remove user
            _context.UserMasters.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
