using System;
using System.Collections.Generic;

namespace Students.Api.DbEntities;

public partial class StudentApplicationReview
{
    public int ReviewId { get; set; }

    public int ApplicationId { get; set; }

    public string Status { get; set; } = null!;

    public string? Comment { get; set; }

    public int? ReviewedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual StudentApplication Application { get; set; } = null!;
}
