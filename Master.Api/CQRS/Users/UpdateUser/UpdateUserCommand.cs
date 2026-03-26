namespace Master.Api.CQRS.Users.UpdateUser
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; } // optional, only update if provided
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
