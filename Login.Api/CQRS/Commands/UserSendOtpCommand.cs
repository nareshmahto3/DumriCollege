using MediatR;

public record UserSendOtpCommand(string PhoneNumber) : IRequest<string>;