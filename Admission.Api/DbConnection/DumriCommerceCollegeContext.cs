using System;
using System.Collections.Generic;
using Admission.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Api.DbConnection;

public partial class DumriCommerceCollegeContext : DbContext
{
    public DumriCommerceCollegeContext()
    {
    }

    public DumriCommerceCollegeContext(DbContextOptions<DumriCommerceCollegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission.Api.DbEntities.Admission> Admission { get; set; }
    public virtual DbSet<Admission.Api.DbEntities.Exam> Exams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NARESH;Database=DumriCommerceCollege;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission.Api.DbEntities.Admission>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<Admission.Api.DbEntities.Exam>(entity =>
        {
            entity.Property(e => e.ExamName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ExamType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Class).IsRequired().HasMaxLength(50);
            entity.Property(e => e.AcademicYear).IsRequired().HasMaxLength(20);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Venue).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Instructions).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
