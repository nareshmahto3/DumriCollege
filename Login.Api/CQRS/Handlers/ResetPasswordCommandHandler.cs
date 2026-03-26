using Login.Api.CQRS.Commands;
using Login.Api.DTOs;
using Login.Api.Infrastructure;
using MediatR;
using System.Text.RegularExpressions;

namespace Login.Api.CQRS.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, LoginResponseDto>
    {
        private readonly UserRepository _repository;

        public ResetPasswordCommandHandler(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponseDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var newPassword = request.ResetPasswordRequest.NewPassword;
            var confirmPassword = request.ResetPasswordRequest.ConfirmPassword;

            // 1️⃣ Check passwords match
            if (newPassword != confirmPassword)
                return new LoginResponseDto { Message = "Passwords do not match" };

            // 2️⃣ Check password strength
            if (!IsStrongPassword(newPassword))
                return new LoginResponseDto
                {
                    Message = "Password is not strong enough. It must be at least 8 characters, include uppercase, lowercase, number, and special character."
                };

            // 3️⃣ Get user by phone
            var user = await _repository.GetByPhoneAsync(request.UserPhone);
            if (user == null)
                return new LoginResponseDto { Message = "User not found" };

            // 4️⃣ Update password
            await _repository.UpdatePasswordAsync(user, newPassword);

            return new LoginResponseDto { Message = "Password reset successful" };
        }

        // Strong password regex: min 8 chars, 1 upper, 1 lower, 1 number, 1 special
        private bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$");
            return regex.IsMatch(password);
        }
    }
}