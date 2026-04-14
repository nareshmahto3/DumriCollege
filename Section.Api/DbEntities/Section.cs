using System;
using System.Collections.Generic;

namespace Section.Api.DbEntities;

public partial class Section
{
    public int SectionId { get; set; }

    public string SectionName { get; set; } = null!;
}
