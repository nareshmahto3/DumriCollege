namespace Master.Api.CQRS.Role
{
    using Master.Api.DbConnection;
    using Master.Api.DTOs;
    using Master.Api.DTOs.Master.Api.DTOs;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetRolesQueryHandler
        : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly DumriCollegeDbContext _context;

        public GetRolesQueryHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleDto>> Handle(
            GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            var roles = await _context.RoleMasters
                                      .AsNoTracking()
                                      .ToListAsync(cancellationToken);

            return roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();
        }
    }
}
