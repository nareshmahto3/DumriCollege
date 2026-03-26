using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string? FacultyName { get; set; }

    public virtual ICollection<FacultyCompulsorySubject> FacultyCompulsorySubjects { get; set; } = new List<FacultyCompulsorySubject>();

    public virtual ICollection<OptionalSubject> OptionalSubjects { get; set; } = new List<OptionalSubject>();

    public virtual ICollection<StudentSubjectSelection> StudentSubjectSelections { get; set; } = new List<StudentSubjectSelection>();
}
