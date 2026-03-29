using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentSubjectSelection
{
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    public int? FacultyId { get; set; }

    public int? CompulsorySubjectId { get; set; }

    public int? OptionalSubject1 { get; set; }

    public int? OptionalSubject2 { get; set; }

    public int? OptionalSubject3 { get; set; }

    public int? AdditionalSubjectId { get; set; }

    public virtual AdditionalSubject? AdditionalSubject { get; set; }

    public virtual StudentApplication? Application { get; set; }

    public virtual CompulsorySubject? CompulsorySubject { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual OptionalSubject? OptionalSubject1Navigation { get; set; }

    public virtual OptionalSubject? OptionalSubject2Navigation { get; set; }

    public virtual OptionalSubject? OptionalSubject3Navigation { get; set; }
}
