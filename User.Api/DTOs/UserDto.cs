namespace User.Api.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserCreateDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
