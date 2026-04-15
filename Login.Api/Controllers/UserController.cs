using Login.Api.CQRS.Commands;
using Login.Api.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequestDto dto)
    {
        var result = await _mediator.Send(
            new UserLoginCommand
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = dto.Role
            }
        ); // ✅ no semicolon inside

        if (result.Message != "Login successful")
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("forgot-password/send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] UserSendOtpDto dto)
    {
        var result = await _mediator.Send(
            new UserSendOtpCommand(dto.PhoneNumber)
        );

        return Ok(new { message = result });
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] UserVerifyOtpDto dto)
    {
        var result = await _mediator.Send(
            new UserVerifyOtpCommand(dto.PhoneNumber, dto.Otp)
        );

        return Ok(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        if (string.IsNullOrEmpty(request.PhoneNumber))
            return BadRequest(new { Message = "User not verified for password reset" });

        // Create command using phone number instead of userId
        var command = new ResetPasswordCommand(request, request.PhoneNumber);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
