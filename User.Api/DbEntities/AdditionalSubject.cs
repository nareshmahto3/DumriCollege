using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class AdditionalSubject
{
    public int AdditionalSubjectId { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelections { get; set; } = new List<StudentSubjectSelection>();
}
