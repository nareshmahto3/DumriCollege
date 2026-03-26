using Login.Api.DTOs;
using Login.Api.Infrastructure;

namespace Login.Api.CQRS.Commands;

public class SendOtpCommandHandler
{
    private readonly StudentRepository _repository;

    public SendOtpCommandHandler(StudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoginResponseDto> HandleAsync(SendOtpCommand command)
    {
        // 1️⃣ Check if student exists
        var student = await _repository.GetByPhoneAsync(command.PhoneNumber);
        if (student == null)
        {
            // Return JSON with message key
            return new LoginResponseDto { Message = "Mobile number not found" };
        }

        // 2️⃣ Generate OTP
        var otp = new Random().Next(100000, 999999);
        var expiry = DateTime.UtcNow.AddMinutes(5);

        // 3️⃣ Save OTP in DB (tracked entity)
        await _repository.UpdateOtpAsync(student, otp, expiry);

        // 4️⃣ Return response
        return new LoginResponseDto
        {
            Message = $"OTP sent to {command.PhoneNumber}",
           
        };
    }
}