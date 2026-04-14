
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Master.Api.DTOs;
    using Master.Api.DbConnection;
using Master.Api.CQRS.Users;

public class GetUsersQueryHandler
        : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly DumriCollegeDbContext _context;

        public GetUsersQueryHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _context.UserMasters
                .Include(u => u.UserRoleMappings)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync(cancellationToken);

            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Roles = u.UserRoleMappings
                            .Select(r => r.Role.RoleName)
                            .ToList()
            }).ToList();
        }
    }

