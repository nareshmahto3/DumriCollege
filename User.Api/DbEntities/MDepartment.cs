using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MDepartment
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;
}
