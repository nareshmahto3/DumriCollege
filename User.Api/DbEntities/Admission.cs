using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Admission
{
    public int Id { get; set; }

    public long? AdmissionId { get; set; }

    public DateTime? Date { get; set; }
}
