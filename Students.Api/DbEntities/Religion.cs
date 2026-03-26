using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class Religion
{
    public int ReligionId { get; set; }

    public string ReligionName { get; set; } = null!;

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
