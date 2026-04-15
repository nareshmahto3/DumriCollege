using MediatR;
namespace Login.Api.Infrastructure   // ✅ Add this
{
    public class UserVerifyOtpCommandHandler : IRequestHandler<UserVerifyOtpCommand, string>
    {
        private readonly UserRepository _repo;

        public UserVerifyOtpCommandHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UserVerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetValidOtpAsync(request.PhoneNumber, request.Otp);

            if (user == null)
                return "Invalid OTP";

            if (user.Otpexpiry < DateTime.UtcNow)
                return "OTP expired";

            return "OTP valid";
        }
    }
}
