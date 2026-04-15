using MediatR;
using Login.Api.DTOs;

namespace Login.Api.CQRS.Commands
{
    public class ResetPasswordCommand : IRequest<LoginResponseDto>
    {
        public ResetPasswordRequestDto ResetPasswordRequest { get; set; }
        public string UserPhone { get; set; } // changed from int UserId

        public ResetPasswordCommand(ResetPasswordRequestDto request, string phone)
        {
            ResetPasswordRequest = request;
            UserPhone = phone;
        }
    }
}