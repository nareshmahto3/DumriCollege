using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
