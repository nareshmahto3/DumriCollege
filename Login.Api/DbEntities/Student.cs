using System;
using System.Collections.Generic;

namespace Login.Api.DbEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? Otp { get; set; }

    public DateTime? Otpexpiry { get; set; }

    public string FullName { get; set; } = null!;

    public string Class { get; set; } = null!;
}
