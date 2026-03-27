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

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamSubject> ExamSubjects { get; set; }

    public virtual DbSet<Notice> Notices { get; set; }

    public virtual DbSet<NoticeAttachment> NoticeAttachments { get; set; }

    public virtual DbSet<StudentDocument> StudentDocuments { get; set; }

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

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.SubjectName).HasMaxLength(200);
            entity.Property(e => e.SubjectCode).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            // Foreign key relationships
            entity.HasOne(s => s.Class)
                  .WithMany()
                  .HasForeignKey(s => s.ClassId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Teacher)
                  .WithMany()
                  .HasForeignKey(s => s.TeacherId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable("Exam");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ExamName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.ExamType).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Class).HasMaxLength(50).IsRequired();
            entity.Property(e => e.AcademicYear).HasMaxLength(20).IsRequired();
            entity.Property(e => e.StartDate).HasColumnType("date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnType("date").IsRequired();
            entity.Property(e => e.Venue).HasMaxLength(250).IsRequired();
            entity.Property(e => e.Instructions).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ExamSubject>(entity =>
        {
            entity.ToTable("ExamSubject");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Subject).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Date).HasColumnType("date").IsRequired();
            entity.Property(e => e.StartTime).HasColumnType("time").IsRequired();
            entity.Property(e => e.EndTime).HasColumnType("time").IsRequired();
            entity.Property(e => e.MaxMarks).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            // Foreign key relationship
            entity.HasOne(es => es.Exam)
                  .WithMany(e => e.ExamSubjects)
                  .HasForeignKey(es => es.ExamId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Notice>(entity =>
        {
            entity.ToTable("Notice");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Priority).HasMaxLength(20).IsRequired();
            entity.Property(e => e.TargetAudience).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PublishDate).HasColumnType("date").IsRequired();
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.NoticeNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<NoticeAttachment>(entity =>
        {
            entity.ToTable("NoticeAttachment");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.FileName).HasMaxLength(255).IsRequired();
            entity.Property(e => e.FilePath).HasMaxLength(500).IsRequired();

            // Foreign key relationship
            entity.HasOne(na => na.Notice)
                  .WithMany(n => n.NoticeAttachments)
                  .HasForeignKey(na => na.NoticeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentDocument>(entity =>
        {
            entity.ToTable("StudentDocument");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StudentId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DocumentName).IsRequired().HasMaxLength(250);
            entity.Property(e => e.DocumentType).IsRequired().HasMaxLength(150);
            entity.Property(e => e.OriginalFileName).IsRequired().HasMaxLength(250);
            entity.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FileSize).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50).HasDefaultValue("pending");
            entity.Property(e => e.UploadDate).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
