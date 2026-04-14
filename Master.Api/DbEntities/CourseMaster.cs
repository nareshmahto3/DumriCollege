using System;
using System.Collections.Generic;

namespace Master.Api.DbEntities;

public partial class CourseMaster
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int? DurationYears { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<ClassMaster> ClassMasters { get; set; } = new List<ClassMaster>();
}
