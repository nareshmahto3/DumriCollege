namespace Master.Api.CQRS.Users
{
    using Master.Api.DbConnection;
    using Master.Api.DbEntities;
    using MediatR;

    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DumriCollegeDbContext _context;

        public CreateUserCommandHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = new UserMaster
            {
                UserName = request.User.UserName,
                Email = request.User.Email,
                PasswordHash = request.User.PasswordHash
            };

            _context.UserMasters.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            // Add Roles
            if (request.User.RoleIds != null)
            {
                foreach (var roleId in request.User.RoleIds)
                {
                    _context.UserRoleMappings.Add(new UserRoleMapping
                    {
                        UserId = user.UserId,
                        RoleId = roleId
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
            }

            return user.UserId;
        }
    }
}
