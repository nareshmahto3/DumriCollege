using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Api.DbEntities;

public partial class User
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? RefreshToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefreshTokenExpiry { get; set; }

}
