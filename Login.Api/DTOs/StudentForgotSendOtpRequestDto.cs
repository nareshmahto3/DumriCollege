namespace Login.Api.DTOs
{
    public class StudentForgotSendOtpRequestDto
    {
        public string OldPhoneNumber { get; set; } = null!;
        public string NewPhoneNumber { get; set; } = null!;

    }
}