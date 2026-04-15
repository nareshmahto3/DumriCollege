namespace Login.Api.CQRS.Commands
{
    public class SendOtpCommand
    {
        public string PhoneNumber { get; set; } = null!;
    }
}