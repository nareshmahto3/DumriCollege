using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class FacultyCompulsorySubject
{
    public int Id { get; set; }

    public int? FacultyId { get; set; }

    public string? SubjectName { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
