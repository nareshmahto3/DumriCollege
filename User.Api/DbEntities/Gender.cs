using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;
}
