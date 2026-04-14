using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class DistrictMaster
{
    public int DistrictId { get; set; }

    public int? StateId { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual StateMaster? State { get; set; }
}
