using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class StateMaster
{
    public int StateId { get; set; }

    public int? CountryId { get; set; }

    public string StateCode { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual CountryMaster? Country { get; set; }

    public virtual ICollection<DistrictMaster> DistrictMasters { get; set; } = new List<DistrictMaster>();
}
