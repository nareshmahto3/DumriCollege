namespace Login.Api.DTOs
{
    public class ResetPasswordRequestDto
    {
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
