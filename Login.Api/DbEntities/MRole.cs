using System;
using System.Collections.Generic;

namespace Login.Api.DbEntities;

public partial class MRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
