namespace Login.Api.DTOs
{
    public class StudentVerifyOtpRequestDto
    {
        public string PhoneNumber { get; set; } = null!;
        public int Otp { get; set; }
    }
}