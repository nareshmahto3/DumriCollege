using System;
using System.Collections.Generic;
using Login.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.DbConnection;

public partial class DumriCollegeDbContext : DbContext
{
    public DumriCollegeDbContext()
    {
    }

    public DumriCollegeDbContext(DbContextOptions<DumriCollegeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=68.178.170.246;Database=dumri_college_db;User Id=dumri_college_user;Password=dumri_college_user@123;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A62AD0F38");

            entity.ToTable("M_Roles");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616039D0223F").IsUnique();

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160B6EF6C53").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99285C830A");

            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Otpexpiry).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C89A2659C");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4638FDA6E").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48FAACB5F").IsUnique();

            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Otp).HasColumnName("OTP");
            entity.Property(e => e.Otpexpiry)
                .HasColumnType("datetime")
                .HasColumnName("OTPExpiry");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__477199F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
