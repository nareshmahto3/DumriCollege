using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class CountryMaster
{
    public int CountryId { get; set; }

    public string CountryCode { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();
}
