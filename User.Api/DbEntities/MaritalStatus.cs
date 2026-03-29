using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MaritalStatus
{
    public int MaritalStatusId { get; set; }

    public string MaritalStatusName { get; set; } = null!;
}
