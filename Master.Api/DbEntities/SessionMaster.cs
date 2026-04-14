using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class SessionMaster
{
    public int SessionId { get; set; }

    public string SessionName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }
}
