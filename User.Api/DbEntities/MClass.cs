using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MClass
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;
}
