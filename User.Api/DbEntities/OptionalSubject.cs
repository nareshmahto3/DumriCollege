using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class OptionalSubject
{
    public int OptionalSubjectId { get; set; }

    public int? FacultyId { get; set; }

    public string? SubjectName { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelectionOptionalSubject1Navigations { get; set; } = new List<StudentSubjectSelection>();

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelectionOptionalSubject2Navigations { get; set; } = new List<StudentSubjectSelection>();

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelectionOptionalSubject3Navigations { get; set; } = new List<StudentSubjectSelection>();
}
