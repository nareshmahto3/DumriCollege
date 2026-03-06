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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
