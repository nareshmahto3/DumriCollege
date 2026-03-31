using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class CollegeUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public string? Otp { get; set; }

    public DateTime? OtpExpiry { get; set; }

    public virtual ICollection<AdminDetail> AdminDetails { get; set; } = new List<AdminDetail>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();

    public virtual ICollection<TeacherDetail> TeacherDetails { get; set; } = new List<TeacherDetail>();
}
