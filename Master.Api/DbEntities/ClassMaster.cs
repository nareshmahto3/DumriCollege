using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master.Api.DbEntities;

public partial class ClassMaster
{
    public int ClassId { get; set; }

    public int? CourseId { get; set; }

    public string ClassName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual CourseMaster? Course { get; set; }
   
}

