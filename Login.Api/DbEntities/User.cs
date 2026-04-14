using System;
using System.Collections.Generic;

namespace Login.Api.DbEntities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Otp { get; set; }

    public DateTime? OtpExpiry { get; set; }

    public virtual Role? Role { get; set; }


}
