using MediatR;

namespace Login.Api.Infrastructure   // ✅ Add this
{
    public class UserSendOtpCommandHandler : IRequestHandler<UserSendOtpCommand, string>
    {
        private readonly UserRepository _repo;

        public UserSendOtpCommandHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UserSendOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByPhoneAsync(request.PhoneNumber);

            if (user == null)
                return "User not found";

            var otp = new Random().Next(100000, 999999);

            await _repo.SaveOtpAsync(request.PhoneNumber, otp);

            Console.WriteLine($"OTP: {otp}");

            return "OTP sent successfully";
        }
    }
}