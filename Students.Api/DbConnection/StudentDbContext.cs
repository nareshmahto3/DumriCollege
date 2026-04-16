using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Students.Api.DbEntities;

namespace Students.Api.DbConnection;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalSubject> AdditionalSubjects { get; set; }

    public virtual DbSet<BloodGroup> BloodGroups { get; set; }

    public virtual DbSet<Caste> Castes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ClassMaster> ClassMasters { get; set; }

    public virtual DbSet<CompulsorySubject> CompulsorySubjects { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyCompulsorySubject> FacultyCompulsorySubjects { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<OptionalSubject> OptionalSubjects { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<StudentApplication> StudentApplications { get; set; }

    public virtual DbSet<StudentCertificate> StudentCertificates { get; set; }

    public virtual DbSet<StudentDocumentVerification> StudentDocumentVerifications { get; set; }

    public virtual DbSet<StudentExamDetail> StudentExamDetails { get; set; }

    public virtual DbSet<StudentSubjectSelection> StudentSubjectSelections { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=Deep\\SQLEXPRESS;Database=dumrideep;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalSubject>(entity =>
        {
            entity.HasKey(e => e.AdditionalSubjectId).HasName("PK__Addition__6290F39200874C76");

            entity.Property(e => e.SubjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<BloodGroup>(entity =>
        {
            entity.HasKey(e => e.BloodGroupId).HasName("PK__BloodGro__4398C68F4EC8A1BD");

            entity.Property(e => e.BloodGroupName).HasMaxLength(10);
        });

        modelBuilder.Entity<Caste>(entity =>
        {
            entity.HasKey(e => e.CasteId).HasName("PK__Castes__463E23ECA8664F39");

            entity.Property(e => e.CasteName).HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BB021A1CF");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ClassMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClassMas__3214EC07B5C825E3");

            entity.ToTable("ClassMaster");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CompulsorySubject>(entity =>
        {
            entity.HasKey(e => e.CompulsorySubjectId).HasName("PK__Compulso__1674A19700BC61B3");

            entity.Property(e => e.SubjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Faculty__306F630ED28845F7");

            entity.ToTable("Faculty");

            entity.Property(e => e.FacultyName).HasMaxLength(50);
        });

        modelBuilder.Entity<FacultyCompulsorySubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FacultyC__3214EC074966BF4D");

            entity.Property(e => e.SubjectName).HasMaxLength(100);

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyCompulsorySubjects)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__FacultyCo__Facul__3B75D760");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Genders__4E24E9F7FEB5B967");

            entity.Property(e => e.GenderName).HasMaxLength(50);
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.HasKey(e => e.MaritalStatusId).HasName("PK__MaritalS__C8B1BA72B19E5EA0");

            entity.Property(e => e.MaritalStatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<OptionalSubject>(entity =>
        {
            entity.HasKey(e => e.OptionalSubjectId).HasName("PK__Optional__AD11115E2EBA80A0");

            entity.Property(e => e.SubjectName).HasMaxLength(100);

            entity.HasOne(d => d.Faculty).WithMany(p => p.OptionalSubjects)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__OptionalS__Facul__3E52440B");
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.HasKey(e => e.ReligionId).HasName("PK__Religion__D3ADAB6A7956E548");

            entity.Property(e => e.ReligionName).HasMaxLength(100);
        });

        modelBuilder.Entity<StudentApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__StudentA__C93A4C9927D78FAC");

            entity.ToTable("StudentApplication");

            entity.Property(e => e.AadhaarNumber).HasMaxLength(20);
            entity.Property(e => e.ApplicationNo).HasMaxLength(50);
            entity.Property(e => e.ApplicationStatus).HasMaxLength(20);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF__StudentAp__Creat__4316F928")
                .HasColumnType("datetime");
            entity.Property(e => e.FatherName).HasMaxLength(100);
            entity.Property(e => e.GuardianOccupation).HasMaxLength(200);
            entity.Property(e => e.Height).HasMaxLength(10);
            entity.Property(e => e.IdentificationMark).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LocalAddress).HasMaxLength(300);
            entity.Property(e => e.MobileNumber).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MotherName).HasMaxLength(100);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PermanentAddress).HasMaxLength(300);
            entity.Property(e => e.RegistrationNo).HasMaxLength(50);
            entity.Property(e => e.StudentName).HasMaxLength(100);
            entity.Property(e => e.Weight).HasMaxLength(10);

            entity.HasOne(d => d.BloodGroup).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.BloodGroupId)
                .HasConstraintName("FK_StudentApplication_BloodGroups");

            entity.HasOne(d => d.Caste).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.CasteId)
                .HasConstraintName("FK_StudentApplication_Castes");

            entity.HasOne(d => d.Category).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_StudentApplication_Categories");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_StudentApplication_Class");

            entity.HasOne(d => d.Gender).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_StudentApplication_Genders");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK_StudentApplication_MaritalStatuses");

            entity.HasOne(d => d.Religion).WithMany(p => p.StudentApplications)
                .HasForeignKey(d => d.ReligionId)
                .HasConstraintName("FK_StudentApplication_Religions");
        });

        modelBuilder.Entity<StudentCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__StudentC__BBF8A7C16344AFBE");

            entity.Property(e => e.CertificateNumber).HasMaxLength(100);
            entity.Property(e => e.CertificateType).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FilePath).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IssuedBy).HasMaxLength(200);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Application).WithMany(p => p.StudentCertificates)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__StudentCe__Appli__52593CB8");
        });

        modelBuilder.Entity<StudentDocumentVerification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentD__3214EC0772E1BE93");

            entity.ToTable("StudentDocumentVerification");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RejectReason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Version).HasDefaultValue(1);

            entity.HasOne(d => d.Application).WithMany(p => p.StudentDocumentVerifications)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentDo__Appli__2180FB33");

            entity.HasOne(d => d.Certificate).WithMany(p => p.StudentDocumentVerifications)
                .HasForeignKey(d => d.CertificateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentDo__Certi__208CD6FA");
        });

        modelBuilder.Entity<StudentExamDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentE__3214EC079332A8E1");

            entity.Property(e => e.BoardCouncil).HasMaxLength(200);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DivisionOrRank).HasMaxLength(50);
            entity.Property(e => e.ExamName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SchoolCollege).HasMaxLength(200);
            entity.Property(e => e.Subjects).HasMaxLength(200);

            entity.HasOne(d => d.Application).WithMany(p => p.StudentExamDetails)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__StudentEx__Appli__4E88ABD4");
        });

        modelBuilder.Entity<StudentSubjectSelection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentS__3214EC0729DE1A78");

            entity.ToTable("StudentSubjectSelection");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AdditionalSubject).WithMany(p => p.StudentSubjectSelections)
                .HasForeignKey(d => d.AdditionalSubjectId)
                .HasConstraintName("FK__StudentSu__Addit__4BAC3F29");

            entity.HasOne(d => d.Application).WithMany(p => p.StudentSubjectSelections)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__StudentSu__Appli__45F365D3");

            entity.HasOne(d => d.CompulsorySubject).WithMany(p => p.StudentSubjectSelections)
                .HasForeignKey(d => d.CompulsorySubjectId)
                .HasConstraintName("FK__StudentSu__Compu__47DBAE45");

            entity.HasOne(d => d.Faculty).WithMany(p => p.StudentSubjectSelections)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__StudentSu__Facul__46E78A0C");

            entity.HasOne(d => d.OptionalSubject1Navigation).WithMany(p => p.StudentSubjectSelectionOptionalSubject1Navigations)
                .HasForeignKey(d => d.OptionalSubject1)
                .HasConstraintName("FK__StudentSu__Optio__48CFD27E");

            entity.HasOne(d => d.OptionalSubject2Navigation).WithMany(p => p.StudentSubjectSelectionOptionalSubject2Navigations)
                .HasForeignKey(d => d.OptionalSubject2)
                .HasConstraintName("FK__StudentSu__Optio__49C3F6B7");

            entity.HasOne(d => d.OptionalSubject3Navigation).WithMany(p => p.StudentSubjectSelectionOptionalSubject3Navigations)
                .HasForeignKey(d => d.OptionalSubject3)
                .HasConstraintName("FK__StudentSu__Optio__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
