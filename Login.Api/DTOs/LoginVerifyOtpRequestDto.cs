namespace Login.Api.DTOs
{
    public class LoginVerifyOtpRequestDto
    {
        public string PhoneNumber { get; set; } = null!;
        public int Otp { get; set; }
    }
}
