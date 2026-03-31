using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MQualification
{
    public int Id { get; set; }

    public string QualificationName { get; set; } = null!;
}
