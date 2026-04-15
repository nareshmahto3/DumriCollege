namespace Login.Api.DTOs
{
    public class LoginResponseDto
    {
        public string Message { get; set; }

        // Common fields
        public string? FullName { get; set; }
        public string? Class { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Username { get; set; }
        public string? Role { get; set; }

        public string? Subject { get; set; }
    }
}