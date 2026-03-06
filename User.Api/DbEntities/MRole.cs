using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;
}
