using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class MaritalStatus
{
    public int MaritalStatusId { get; set; }

    public string MaritalStatusName { get; set; } = null!;

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
