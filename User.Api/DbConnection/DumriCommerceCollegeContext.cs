using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using User.Api.DbEntities;

namespace User.Api.DbConnection;

public partial class DumriCommerceCollegeContext : DbContext
{
    public DumriCommerceCollegeContext()
    {
    }

    public DumriCommerceCollegeContext(DbContextOptions<DumriCommerceCollegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<User.Api.DbEntities.User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NARESH;Database=DumriCommerceCollege;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.ToTable("Admission");

            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<MRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("M_Role");

            entity.Property(e => e.RoleName).HasMaxLength(10);
        });

        modelBuilder.Entity<User.Api.DbEntities.User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RefreshToken).HasMaxLength(250);
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(150);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Qualification).HasMaxLength(100);
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Experience).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(10);
            entity.Property(e => e.BloodGroup).HasMaxLength(10);
            entity.Property(e => e.Religion).HasMaxLength(50);
            entity.Property(e => e.EmergencyContact).HasMaxLength(20);
            entity.Property(e => e.Subjects).HasMaxLength(500);
            entity.Property(e => e.ShortBio).HasMaxLength(500);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.JoiningDate).HasColumnType("date");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.Section).HasMaxLength(10);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
            entity.Property(e => e.AcademicYear).HasMaxLength(20);
            entity.Property(e => e.Subjects).HasMaxLength(1000);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            // Foreign key relationship
            entity.HasOne(c => c.ClassTeacher)
                  .WithMany()
                  .HasForeignKey(c => c.ClassTeacherId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
