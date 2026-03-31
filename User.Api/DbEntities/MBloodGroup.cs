using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MBloodGroup
{
    public int Id { get; set; }

    public string BloodGroup { get; set; } = null!;
}
