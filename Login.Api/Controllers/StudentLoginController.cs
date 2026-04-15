using Login.Api.DbConnection;
using Login.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentLoginController : ControllerBase
{
    private readonly DumriCollegeDbContext _context;

    public StudentLoginController(DumriCollegeDbContext context)
    {
        _context = context;
    }

    // ============================
    // SEND LOGIN OTP
    // ============================
    [HttpPost("send-login-otp")]
    public async Task<IActionResult> SendLoginOtp([FromBody] LoginOtpRequestDto request)
    {
        if (string.IsNullOrEmpty(request.PhoneNumber))
            return BadRequest(new { message = "Phone number is required" });

        var student = await _context.Students
            .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

        if (student == null)
            return BadRequest(new { message = "Phone number not registered" });

        var otp = new Random().Next(100000, 999999);

        student.Otp = otp;

        // 🔥 FIX: Always use UTC
        student.Otpexpiry = DateTime.UtcNow.AddMinutes(10);

        await _context.SaveChangesAsync();

        Console.WriteLine($"OTP for {request.PhoneNumber}: {otp}");

        return Ok(new { message = "OTP sent successfully" });
    }

    // ============================
    // VERIFY LOGIN OTP
    // ============================
    [HttpPost("verify-login-otp")]
    public IActionResult VerifyLoginOtp([FromBody] LoginVerifyOtpRequestDto request)
    {
        string Normalize(string number)
        {
            return number.Replace("+91", "")
                         .Replace("91", "")
                         .Replace(" ", "")
                         .Trim();
        }

        var student = _context.Students
            .AsEnumerable()
            .FirstOrDefault(s =>
                Normalize(s.PhoneNumber) == Normalize(request.PhoneNumber)
            );

        if (student == null)
            return BadRequest(new { message = "Student not found" });

        if (student.Otp != request.Otp)
            return BadRequest(new { message = "Invalid OTP" });

        // 🔥 FIX: Use UTC + null check
        if (student.Otpexpiry == null || student.Otpexpiry < DateTime.UtcNow)
            return BadRequest(new { message = "OTP expired" });

        return Ok(new
        {
            message = "Login successful",
            data = new
            {
                fullName = student.FullName,
                @class = student.Class
            }
        });
    }

    // ============================
    // FORGOT OTP (UNCHANGED)
    // ============================
    [HttpPost("Forgot-send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] StudentForgotSendOtpRequestDto dto)
    {
        if (string.IsNullOrEmpty(dto.OldPhoneNumber) || string.IsNullOrEmpty(dto.NewPhoneNumber))
        {
            return BadRequest(new { message = "Phone numbers are required" });
        }

        if (dto.OldPhoneNumber == dto.NewPhoneNumber)
        {
            return BadRequest(new { message = "New phone number must be different" });
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.PhoneNumber == dto.OldPhoneNumber);

        if (student == null)
        {
            return NotFound(new { message = "Old mobile number not found" });
        }

        var exists = await _context.Students
            .AnyAsync(s => s.PhoneNumber == dto.NewPhoneNumber);

        if (exists)
        {
            return BadRequest(new { message = "New phone number already in use" });
        }

        var otp = new Random().Next(100000, 999999);

        student.Otp = otp;

        // 🔥 FIX: use UTC
        student.Otpexpiry = DateTime.UtcNow.AddMinutes(10);

        await _context.SaveChangesAsync();

        return Ok(new { message = "OTP sent to new mobile number" });
    }

    [HttpPost("Forgot-verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] StudentForgotVerifyOtpRequestDto dto)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.PhoneNumber == dto.OldPhoneNumber);

        if (student == null)
        {
            return NotFound(new { message = "Old mobile number not found" });
        }

        if (student.Otp != dto.Otp || student.Otpexpiry == null || student.Otpexpiry < DateTime.UtcNow)
        {
            return BadRequest(new { message = "Invalid or expired OTP" });
        }

        student.PhoneNumber = dto.NewPhoneNumber;
        student.Otp = null;
        student.Otpexpiry = null;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Phone number updated successfully",
            data = new
            {
                student.StudentId,
                student.FullName,
                student.PhoneNumber,
                student.Class
            }
        });
    }
}