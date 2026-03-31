using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MCity
{
    public int Id { get; set; }

    public string DistrictName { get; set; } = null!;

    public int StateId { get; set; }

    public virtual MState State { get; set; } = null!;
}
