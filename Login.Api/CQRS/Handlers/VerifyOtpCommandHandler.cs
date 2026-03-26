using Login.Api.DTOs;
using Login.Api.Infrastructure;

namespace Login.Api.CQRS.Commands;

public class VerifyOtpCommandHandler
{
    private readonly StudentRepository _repository;

    public VerifyOtpCommandHandler(StudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoginResponseDto> HandleAsync(VerifyOtpCommand command)
    {
        // 1️⃣ Fetch student by phone number
        var student = await _repository.GetByPhoneAsync(command.PhoneNumber);
        if (student == null)
        {
            // Return JSON with message key
            return new LoginResponseDto { Message = "Mobile number not found" };
        }

        // 2️⃣ Check OTP validity
        if (student.Otp != command.Otp || student.OtpExpiry < DateTime.UtcNow)
        {
            return new LoginResponseDto { Message = "Invalid or expired OTP" };
        }

        // 3️⃣ OTP is correct → clear it
        await _repository.UpdateOtpAsync(student, null, null);

        // 4️⃣ Return success
        return new LoginResponseDto
        {
            Message = "Login successful"
           
        };
    }
}