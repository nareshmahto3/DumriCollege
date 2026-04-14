using System;
using System.Collections.Generic;

namespace Frontend.Api.DbEntities;

public partial class Contact
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string EnquiryId { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
