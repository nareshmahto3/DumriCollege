using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Student
{
    public int StudentId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? Otp { get; set; }

    public DateTime? Otpexpiry { get; set; }
}
