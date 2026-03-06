namespace User.Api.DTOs
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
