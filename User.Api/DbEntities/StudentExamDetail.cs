using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class StudentExamDetail
{
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    public string? SchoolCollege { get; set; }

    public string? BoardCouncil { get; set; }

    public string? ExamName { get; set; }

    public int? YearOfPassing { get; set; }

    public string? DivisionOrRank { get; set; }

    public string? Subjects { get; set; }

    public virtual StudentApplication? Application { get; set; }
}
