using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MTargetAudience
{
    public int Id { get; set; }

    public string AudienceName { get; set; } = null!;
}
