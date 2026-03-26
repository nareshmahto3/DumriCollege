using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class UserRoleMapping
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual RoleMaster Role { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;
}
