using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class SectionMaster
{
    public int SectionId { get; set; }

    public string SectionName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }
}
