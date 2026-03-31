using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Role1
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
