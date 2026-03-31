using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MDesignation
{
    public int Id { get; set; }

    public string DesigName { get; set; } = null!;
}
