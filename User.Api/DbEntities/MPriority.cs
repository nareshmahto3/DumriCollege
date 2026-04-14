using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MPriority
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;
}
