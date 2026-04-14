using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class CompulsorySubject
{
    public int CompulsorySubjectId { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelections { get; set; } = new List<StudentSubjectSelection>();
}
