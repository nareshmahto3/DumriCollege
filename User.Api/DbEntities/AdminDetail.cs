using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class AdminDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Department { get; set; }

    public string? Permissions { get; set; }

    public virtual CollegeUser User { get; set; } = null!;
}
