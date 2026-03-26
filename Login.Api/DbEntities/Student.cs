using System;
using System.Collections.Generic;

namespace Login.Api.DbEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Otp { get; set; }

    public DateTime? OtpExpiry { get; set; }
}
