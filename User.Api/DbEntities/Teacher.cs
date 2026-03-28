using System;
using System.Collections.Generic;

namespace User.Api.DbEntities;

public partial class Teacher
{
    public int Id { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Qualification { get; set; }

    public string? Designation { get; set; }

    public string? Department { get; set; }

    public DateTime? JoiningDate { get; set; }

    public string? Experience { get; set; }

    public decimal? Salary { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? BloodGroup { get; set; }

    public string? Religion { get; set; }

    public string? EmergencyContact { get; set; }

    public string? Subjects { get; set; }

    public string? ShortBio { get; set; }

    public string? PhotoPath { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}
