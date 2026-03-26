using Login.Api.DbConnection;
using Login.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
 

[Route("api/[controller]")]
[ApiController]
public class StudentLoginController : ControllerBase
{
    private readonly LoginContext _context; // ✅ your EF DbContext

    public StudentLoginController(LoginContext context)
    {
        _context = context; // ✅ injected via constructor
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] StudentSendOtpRequestDto dto)
    {
        // Check if student exists
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.PhoneNumber == dto.PhoneNumber);

        if (student == null)
        {
            return NotFound(new { message = "Mobile number not found" });
        }

        // Generate OTP
        var otp = new Random().Next(100000, 999999);

        // Save OTP and expiry
        student.Otp = otp;
        student.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
        await _context.SaveChangesAsync();

        // Optionally send OTP via SMS

        return Ok(new { message = "OTP Sent Successfully!" });
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] StudentVerifyOtpRequestDto dto)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.PhoneNumber == dto.PhoneNumber);

        if (student == null)
        {
            return NotFound(new { message = "Mobile Number not found" });
        }

        if (student.Otp != dto.Otp || student.OtpExpiry < DateTime.UtcNow)
        {
            return BadRequest(new { message = "Invalid or expired OTP" });
        }

        // Clear OTP after successful login
        student.Otp = null;
        student.OtpExpiry = null;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Login successful" });
    }
}