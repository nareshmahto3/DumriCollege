using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class SubjectMaster
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }
}
