using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admission.Api.DbEntities;

public partial class Admission
{
    [Key]
    public int Id { get; set; }

    public long AdmissionId { get; set; }

    public DateTime? Date { get; set; }
}
