using Microsoft.EntityFrameworkCore;
using Models;
using Type = Models.Type;

namespace DAL.MySQLProvider.taskmanagement
{
    public partial class taskmanagementContext : DbContext
    {
        public taskmanagementContext()
        {
        }

        public taskmanagementContext(DbContextOptions<taskmanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignedtasks> Assignedtasks { get; set; }
        public virtual DbSet<Complexity> Complexity { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Eventlog> Eventlog { get; set; }
        public virtual DbSet<Qualifications> Qualifications { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Welcome01_;database=taskmanagement", x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignedtasks>(entity =>
            {
                entity.ToTable("assignedtasks");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("id_Employee");

                entity.HasIndex(e => e.IdResult)
                    .HasName("id_Result");

                entity.HasIndex(e => e.IdTask)
                    .HasName("id_Task");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DateEnd)
                    .HasColumnName("Date_End")
                    .HasColumnType("date");

                entity.Property(e => e.DateStart)
                    .HasColumnName("Date_Start")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee)
                    .HasColumnName("id_Employee")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResult)
                    .HasColumnName("id_Result")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTask)
                    .HasColumnName("id_Task")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Assignedtasks)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("assignedtasks_ibfk_2");

                entity.HasOne(d => d.IdResultNavigation)
                    .WithMany(p => p.Assignedtasks)
                    .HasForeignKey(d => d.IdResult)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("assignedtasks_ibfk_3");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Assignedtasks)
                    .HasForeignKey(d => d.IdTask)
                    .HasConstraintName("assignedtasks_ibfk_1");
            });

            modelBuilder.Entity<Complexity>(entity =>
            {
                entity.ToTable("complexity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComplexityQual1)
                    .HasColumnName("Complexity_Qual1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComplexityQual2)
                    .HasColumnName("Complexity_Qual2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComplexityQual3)
                    .HasColumnName("Complexity_Qual3")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComplexityQual4)
                    .HasColumnName("Complexity_Qual4")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.IdQualification)
                    .HasName("id_Qualification");

                entity.HasIndex(e => e.IdType)
                    .HasName("id_Type");

                entity.HasIndex(e => e.Login)
                    .HasName("Login")
                    .IsUnique();

                entity.HasIndex(e => e.Password)
                    .HasName("Password")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdQualification)
                    .HasColumnName("id_Qualification")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdType)
                    .HasColumnName("id_Type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdQualificationNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdQualification)
                    .HasConstraintName("employees_ibfk_1");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("employees_ibfk_2");
            });

            modelBuilder.Entity<Eventlog>(entity =>
            {
                entity.ToTable("eventlog");

                entity.HasIndex(e => e.IdCurrentStatus)
                    .HasName("id_CurrentStatus");

                entity.HasIndex(e => e.IdEmployee)
                    .HasName("id_Employee");

                entity.HasIndex(e => e.IdLastStatus)
                    .HasName("id_LastStatus");

                entity.HasIndex(e => e.IdTask)
                    .HasName("id_Task");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("timestamp");

                entity.Property(e => e.IdCurrentStatus)
                    .HasColumnName("id_CurrentStatus")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdEmployee)
                    .HasColumnName("id_Employee")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdLastStatus)
                    .HasColumnName("id_LastStatus")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTask)
                    .HasColumnName("id_Task")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdCurrentStatusNavigation)
                    .WithMany(p => p.EventlogIdCurrentStatusNavigation)
                    .HasForeignKey(d => d.IdCurrentStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("eventlog_ibfk_2");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Eventlog)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("eventlog_ibfk_3");

                entity.HasOne(d => d.IdLastStatusNavigation)
                    .WithMany(p => p.EventlogIdLastStatusNavigation)
                    .HasForeignKey(d => d.IdLastStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("eventlog_ibfk_1");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.Eventlog)
                    .HasForeignKey(d => d.IdTask)
                    .HasConstraintName("eventlog_ibfk_4");
            });

            modelBuilder.Entity<Qualifications>(entity =>
            {
                entity.ToTable("qualifications");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Coefficient).HasColumnType("decimal(2,1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Results>(entity =>
            {
                entity.ToTable("results");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResultQual1)
                    .HasColumnName("Result_Qual1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResultQual2)
                    .HasColumnName("Result_Qual2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResultQual3)
                    .HasColumnName("Result_Qual3")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResultQual4)
                    .HasColumnName("Result_Qual4")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks");

                entity.HasIndex(e => e.IdComplexity)
                    .HasName("id_Complexity");

                entity.HasIndex(e => e.IdParentTask)
                    .HasName("id_ParentTask");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateDelivery)
                    .HasColumnName("Date_Delivery")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdComplexity)
                    .HasColumnName("id_Complexity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdParentTask)
                    .HasColumnName("id_ParentTask")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTaskManager)
                    .HasColumnName("id_TaskManager")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdComplexityNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdComplexity)
                    .HasConstraintName("tasks_ibfk_2");

                entity.HasOne(d => d.IdParentTaskNavigation)
                    .WithMany(p => p.InverseIdParentTaskNavigation)
                    .HasForeignKey(d => d.IdParentTask)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tasks_ibfk_1");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
