using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MState
{
    public int Id { get; set; }

    public string StateName { get; set; } = null!;

    public virtual ICollection<MCity> MCities { get; set; } = new List<MCity>();
}
