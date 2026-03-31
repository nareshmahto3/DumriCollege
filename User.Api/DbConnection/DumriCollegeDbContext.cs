using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using User.Api.DbEntities;

namespace User.Api.DbConnection;

public partial class DumriCollegeDbContext : DbContext
{
    public DumriCollegeDbContext()
    {
    }

    public DumriCollegeDbContext(DbContextOptions<DumriCollegeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminDetail> AdminDetails { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<CollegeUser> CollegeUsers { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }

    public virtual DbSet<FeeBreakdown> FeeBreakdowns { get; set; }

    public virtual DbSet<FeeHead> FeeHeads { get; set; }

    public virtual DbSet<Invigilator> Invigilators { get; set; }

    public virtual DbSet<MAcademicYear> MAcademicYears { get; set; }

    public virtual DbSet<MBloodGroup> MBloodGroups { get; set; }

    public virtual DbSet<MCategory> MCategories { get; set; }

    public virtual DbSet<MCity> MCities { get; set; }

    public virtual DbSet<MClass> MClasses { get; set; }

    public virtual DbSet<MDepartment> MDepartments { get; set; }

    public virtual DbSet<MDesignation> MDesignations { get; set; }

    public virtual DbSet<MExamType> MExamTypes { get; set; }

    public virtual DbSet<MGender> MGenders { get; set; }

    public virtual DbSet<MMonth> MMonths { get; set; }

    public virtual DbSet<MPriority> MPriorities { get; set; }

    public virtual DbSet<MQualification> MQualifications { get; set; }

    public virtual DbSet<MReligion> MReligions { get; set; }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<MState> MStates { get; set; }

    public virtual DbSet<MTargetAudience> MTargetAudiences { get; set; }

    public virtual DbSet<MTeacher> MTeachers { get; set; }

    public virtual DbSet<MTeachingSubject> MTeachingSubjects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentDetail> StudentDetails { get; set; }

    public virtual DbSet<StudentFee> StudentFees { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Subject1> Subjects1 { get; set; }

    public virtual DbSet<TeacherDetail> TeacherDetails { get; set; }

    public virtual DbSet<DbEntities.User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=68.178.170.246;Database=dumri_college_db;user id=dumri_college_user;password=dumri_college_user@123;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admin_de__3214EC079E389221");

            entity.ToTable("admin_details");

            entity.HasIndex(e => e.Email, "UQ__admin_de__AB6E61648DA30215").IsUnique();

            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Permissions)
                .HasColumnType("text")
                .HasColumnName("permissions");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AdminDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__admin_det__user___27F8EE98");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__class__FDF47986438D02F9");

            entity.ToTable("class");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("class_name");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__class__course_id__440B1D61");
        });

        modelBuilder.Entity<CollegeUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__college___B9BE370F6A8E7EAF");

            entity.ToTable("college_user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Otp)
                .HasMaxLength(10)
                .HasColumnName("otp");
            entity.Property(e => e.OtpExpiry)
                .HasColumnType("datetime")
                .HasColumnName("otp_expiry");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.CollegeUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__college_u__role___0E6E26BF");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC07D98C1510");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EnquiryId).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Subject).HasMaxLength(200);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__course__8F1EF7AE34B81186");

            entity.ToTable("course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course_name");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SessionId).HasColumnName("session_id");

            entity.HasOne(d => d.Session).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__session___3F466844");
        });

        modelBuilder.Entity<ExamSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__ExamSche__9C8A5B491180B7CF");

            entity.ToTable("ExamSchedule");

            entity.HasOne(d => d.Class).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__ExamSched__Class__03F0984C");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__ExamSched__ExamI__02FC7413");

            entity.HasOne(d => d.Invigilator).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.InvigilatorId)
                .HasConstraintName("FK__ExamSched__Invig__06CD04F7");

            entity.HasOne(d => d.Room).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__ExamSched__RoomI__05D8E0BE");

            entity.HasOne(d => d.Subject).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__ExamSched__Subje__04E4BC85");
        });

        modelBuilder.Entity<FeeBreakdown>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FeeBreak__3214EC07DDC025A9");

            entity.ToTable("FeeBreakdown");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FeeType)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FeeHead>(entity =>
        {
            entity.ToTable("FeeHead");

            entity.Property(e => e.FeeHeadTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<Invigilator>(entity =>
        {
            entity.HasKey(e => e.InvigilatorId).HasName("PK__Invigila__472FACEFB0CE3EC0");

            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MAcademicYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Academic__3214EC075F848FA7");

            entity.ToTable("M_AcademicYear");

            entity.Property(e => e.YearName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MBloodGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Blood___3214EC27449B431D");

            entity.ToTable("M_Blood_Group");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Blood_Group");
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
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<MCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_City__3214EC075A60C038");

            entity.ToTable("M_City");

            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.MCities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__M_City__StateID__762C88DA");
        });

        modelBuilder.Entity<MClass>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0595CB9F9");

            entity.ToTable("M_Classes");

            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3214EC07D7908CD1");

            entity.ToTable("M_Departments");

            entity.HasIndex(e => e.DepartmentName, "UQ__departme__D949CC34C8400B2E").IsUnique();

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MDesignation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Design__3213E83F9CECADA5");

            entity.ToTable("M_Designations");

            entity.HasIndex(e => e.DesigName, "UQ__M_Design__4D5D7C6024A28AC8").IsUnique();

            entity.Property(e => e.DesigName)
                .HasMaxLength(50)
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

        modelBuilder.Entity<MGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Gender__3214EC27BD6BEB67");

            entity.ToTable("M_Gender");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MMonth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Month__3214EC07443E9D04");

            entity.ToTable("M_Month");

            entity.Property(e => e.MonthName)
                .HasMaxLength(20)
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

        modelBuilder.Entity<MQualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Qualif__3213E83FE601EA9D");

            entity.ToTable("M_Qualifications");

            entity.HasIndex(e => e.QualificationName, "UQ__M_Qualif__49C0FCDB5FDB5AAD").IsUnique();

            entity.Property(e => e.QualificationName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MReligion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Religi__3214EC275C079325");

            entity.ToTable("M_Religion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A62AD0F38");

            entity.ToTable("M_Roles");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616039D0223F").IsUnique();

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160B6EF6C53").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<MState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_States__3214EC07FC12A616");

            entity.ToTable("M_States");

            entity.HasIndex(e => e.StateName, "UQ__M_States__5547631502029459").IsUnique();

            entity.Property(e => e.StateName)
                .HasMaxLength(100)
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

        modelBuilder.Entity<MTeacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Teache__3214EC07B3F80474");

            entity.ToTable("M_Teachers");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmergencyContact).HasMaxLength(20);
            entity.Property(e => e.EmployeeId).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<MTeachingSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Teachi__3214EC07E3BCEF44");

            entity.ToTable("M_Teaching_Subjects");

            entity.HasIndex(e => e.SubjectName, "UQ__M_Teachi__4C5A7D55C2486EF6").IsUnique();

            entity.Property(e => e.SubjectName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__760965CCB2EEBECD");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__32863939C8A43CD6");

            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK__section__F842676A3631622A");

            entity.ToTable("section");

            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SectionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("section_name");

            entity.HasOne(d => d.Class).WithMany(p => p.Sections)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__section__class_i__48CFD27E");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__session__69B13FDC959424F8");

            entity.ToTable("session");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SessionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("session_name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99285C830A");

            entity.Property(e => e.Otpexpiry).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<StudentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__student___3214EC07DFACFB37");

            entity.ToTable("student_details");

            entity.HasIndex(e => e.Email, "UQ__student___AB6E6164226ED19C").IsUnique();

            entity.Property(e => e.Course)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RollNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roll_number");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.StudentDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__student_d__user___2057CCD0");
        });

        modelBuilder.Entity<StudentFee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentF__3214EC07993B3029");

            entity.ToTable("StudentFee");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__subject__5004F660AE281CB6");

            entity.ToTable("subject");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.Class).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__subject__class_i__4D94879B");
        });

        modelBuilder.Entity<Subject1>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A877C24EDB");

            entity.ToTable("Subjects");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Subject1s)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Subjects__ClassI__7A672E12");
        });

        modelBuilder.Entity<TeacherDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teacher___3214EC07562299F9");

            entity.ToTable("teacher_details");

            entity.HasIndex(e => e.Email, "UQ__teacher___AB6E616404A0A119").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TeacherDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__teacher_d__user___24285DB4");
        });

        modelBuilder.Entity<DbEntities.User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C89A2659C");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4638FDA6E").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E48FAACB5F").IsUnique();

            entity.Property(e => e.Otp).HasColumnName("OTP");
            entity.Property(e => e.Otpexpiry)
                .HasColumnType("datetime")
                .HasColumnName("OTPExpiry");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__477199F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
