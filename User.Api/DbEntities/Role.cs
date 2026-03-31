using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<CollegeUser> CollegeUsers { get; set; } = new List<CollegeUser>();
}
