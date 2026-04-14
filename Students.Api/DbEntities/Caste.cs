using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class Caste
{
    public int CasteId { get; set; }

    public string CasteName { get; set; } = null!;

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
