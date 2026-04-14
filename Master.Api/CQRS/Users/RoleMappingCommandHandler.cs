using Master.Api.DbConnection;
using Master.Api.DbEntities;
using MediatR;

namespace Master.Api.CQRS.Users
{
   
    public class RoleMappingCommandHandler
    : IRequestHandler<RoleMappingCommand, bool>
    {
        private readonly DumriCollegeDbContext _context;

        public RoleMappingCommandHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            RoleMappingCommand request,
            CancellationToken cancellationToken)
        {
            var mapping = new UserRoleMapping
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            };

            _context.UserRoleMappings.Add(mapping);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
