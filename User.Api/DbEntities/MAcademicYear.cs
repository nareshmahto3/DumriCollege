using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MAcademicYear
{
    public int Id { get; set; }

    public string YearName { get; set; } = null!;
}
