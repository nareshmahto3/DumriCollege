using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MTeachingSubject
{
    public int Id { get; set; }

    public string SubjectName { get; set; } = null!;
}
