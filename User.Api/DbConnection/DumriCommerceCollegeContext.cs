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

    // ================= EXISTING TABLES =================
    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<User.Api.DbEntities.User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    // ================= NEW MERGED TABLES =================
    public virtual DbSet<MAcademicYear> MAcademicYears { get; set; }
    public virtual DbSet<MCategory> MCategories { get; set; }
    public virtual DbSet<MClass> MClasses { get; set; }
    public virtual DbSet<MExamType> MExamTypes { get; set; }
    public virtual DbSet<MPriority> MPriorities { get; set; }
    public virtual DbSet<MTargetAudience> MTargetAudiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning Move connection string to appsettings.json
        => optionsBuilder.UseSqlServer(
            "Server=68.178.170.246;Database=dumri_college_db;User Id=dumri_college_user;Password=dumri_college_user@123;MultipleActiveResultSets=true;TrustServerCertificate=True;"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ================= EXISTING CONFIG =================
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

        // ================= MERGED CONFIG =================
        modelBuilder.Entity<MAcademicYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Academic__3214EC075F848FA7");
            entity.ToTable("M_AcademicYear");

            entity.Property(e => e.YearName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__M_Catego__19093A0BADD992A2");
            entity.ToTable("M_Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<MClass>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0595CB9F9");
            entity.ToTable("M_Classes");

            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MExamType>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exams__297521C7ED2C611D");
            entity.ToTable("M_ExamType");

            entity.Property(e => e.ExamName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ExamType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MPriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__M_Priori__D0A3D0BEA3204C40");
            entity.ToTable("M_Priority");

            entity.Property(e => e.PriorityName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MTargetAudience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Target__3214EC073A7FA13E");
            entity.ToTable("M_TargetAudience");

            entity.Property(e => e.AudienceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}