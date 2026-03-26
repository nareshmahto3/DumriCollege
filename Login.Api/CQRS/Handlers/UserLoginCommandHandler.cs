using Login.Api.CQRS.Commands;
using Login.Api.DbConnection;
using Login.Api.DbEntities;
using Login.Api.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.CQRS.Handlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoginResponseDto>
    {
        private readonly LoginContext _context;

        public UserLoginCommandHandler(LoginContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

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

            // ❌ Role mismatch
            if (user.Role.RoleName.ToLower() != request.Role.ToLower())
            {
                return new LoginResponseDto
                {
                    Message = "Invalid role selected"
                };
            }

            // ✅ Success
            return new LoginResponseDto
            {
                Message = "Login successful"
            };
        }
    }
}