using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class StudentDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Course { get; set; }

    public int? Semester { get; set; }

    public string? RollNumber { get; set; }

    public virtual CollegeUser User { get; set; } = null!;
}
