using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Session
{
    public int SessionId { get; set; }

    public string SessionName { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
