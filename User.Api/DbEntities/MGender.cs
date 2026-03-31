using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MGender
{
    public int Id { get; set; }

    public string Gender { get; set; } = null!;
}
