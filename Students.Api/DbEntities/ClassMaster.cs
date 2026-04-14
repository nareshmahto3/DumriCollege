using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class ClassMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StudentApplication> StudentApplications { get; set; } = new List<StudentApplication>();
}
