using MediatR;
using Login.Api.DTOs;

namespace Login.Api.CQRS.Commands
{
    public class UserLoginCommand : IRequest<LoginResponseDto>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}