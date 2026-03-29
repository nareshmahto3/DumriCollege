using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Caste
{
    public int CasteId { get; set; }

    public string CasteName { get; set; } = null!;
}
