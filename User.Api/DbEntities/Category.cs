using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
}
