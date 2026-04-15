using Login.Api.CQRS.Commands;
using Login.Api.DbConnection;
using Login.Api.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.CQRS.Handlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoginResponseDto>
    {
        private readonly DumriCollegeDbContext _context;

        public UserLoginCommandHandler(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            // 🔍 Fetch user with role
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            // ❌ User not found
            if (user == null)
            {
                return new LoginResponseDto
                {
                    Message = "User not found"
                };
            }

            // ❌ Password mismatch
            if (user.Password != request.Password)
            {
                return new LoginResponseDto
                {
                    Message = "Invalid password"
                };
            }

            // ❌ Role mismatch (null-safe + case-insensitive)
            if (user.Role == null ||
                !user.Role.RoleName.Equals(request.Role, StringComparison.OrdinalIgnoreCase))
            {
                return new LoginResponseDto
                {
                    Message = "Invalid role selected"
                };
            }

            // ✅ Success
            var roleName = user.Role.RoleName;

            return new LoginResponseDto
            {
                Message = "Login successful",
                Username = user.Username,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = roleName,

                // 🎯 Role-based data
                Subject = roleName.Equals("Teacher", StringComparison.OrdinalIgnoreCase)
                            ? user.Subject
                            : null,

                Class = roleName.Equals("SuperAdmin", StringComparison.OrdinalIgnoreCase)
                            ? "ALL"
                            : null
            };
        }
    }
}