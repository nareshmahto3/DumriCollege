using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class MCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }
}
