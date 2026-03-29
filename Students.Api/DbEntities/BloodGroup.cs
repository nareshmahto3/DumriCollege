using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class BloodGroup
{
    public int BloodGroupId { get; set; }

    public string BloodGroupName { get; set; } = null!;

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
