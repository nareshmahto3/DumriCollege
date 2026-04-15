namespace Login.Api.DTOs
{
    public class StudentForgotVerifyOtpRequestDto
    {
        public string OldPhoneNumber { get; set; } = null!;
        public string NewPhoneNumber { get; set; } = null!;
        public int Otp { get; set; }
    }
}