using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Religion
{
    public int ReligionId { get; set; }

    public string ReligionName { get; set; } = null!;
}
