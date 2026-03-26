using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class BloodGroup
{
    public int BloodGroupId { get; set; }

    public string BloodGroupName { get; set; } = null!;
}
