using MediatR;

public record UserVerifyOtpCommand(string PhoneNumber, int Otp) : IRequest<string>;
