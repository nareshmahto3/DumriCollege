using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class FeeHead
{
    public int FeeHeadId { get; set; }

    public string FeeHeadTitle { get; set; } = null!;

    public bool IsCompulsory { get; set; }
}
