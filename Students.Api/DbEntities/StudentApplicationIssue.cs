using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentApplicationIssue
{
    public int IssueId { get; set; }

    public int ApplicationId { get; set; }

    public string FieldName { get; set; } = null!;

    public string? Comment { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual StudentApplication Application { get; set; } = null!;
}
