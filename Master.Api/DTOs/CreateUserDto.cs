namespace Master.Api.DTOs
{
    public class CreateUserDto
    {

            public string UserName { get; set; } = null!;
            public string? Email { get; set; }
            public string PasswordHash { get; set; } = null!;
            public List<int>? RoleIds { get; set; }
        }
    }

