using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Section
{
    public int SectionId { get; set; }

    public string SectionName { get; set; } = null!;

    public int ClassId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Class Class { get; set; } = null!;
}
