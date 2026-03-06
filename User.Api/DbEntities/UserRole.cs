using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class UserRole
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }
}
