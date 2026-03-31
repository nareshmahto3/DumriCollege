using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class TeacherDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Subject { get; set; }

    public decimal? Salary { get; set; }

    public int? Experience { get; set; }

    public virtual CollegeUser User { get; set; } = null!;
}
