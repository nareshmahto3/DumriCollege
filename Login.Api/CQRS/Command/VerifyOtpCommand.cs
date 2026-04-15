namespace Login.Api.CQRS.Commands
{
    public class VerifyOtpCommand
    {
        public string PhoneNumber { get; set; } = null!;
        public int Otp { get; set; }
    }
}