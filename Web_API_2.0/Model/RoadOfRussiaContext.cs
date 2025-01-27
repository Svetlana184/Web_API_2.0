using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_API_2._0.Model;

public partial class RoadOfRussiaContext : DbContext
{
    public RoadOfRussiaContext()
    {
    }

    public RoadOfRussiaContext(DbContextOptions<RoadOfRussiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventMaterial> EventMaterials { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<WorkingCalendar> WorkingCalendars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TEACHERPC;Initial Catalog=RoadOfRussia;User ID=user12;Password=1234;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.IdCalendar);

            entity.ToTable("Calendar");

            entity.Property(e => e.TypeOfAbsense).HasMaxLength(20);
            entity.Property(e => e.TypeOfEvent).HasMaxLength(50);

            entity.HasOne(d => d.IdAlternateNavigation).WithMany(p => p.CalendarIdAlternateNavigations)
                .HasForeignKey(d => d.IdAlternate)
                .HasConstraintName("FK_Calendar_Employee1");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.CalendarIdEmployeeNavigations)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Calendar_Employee");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("FK_Calendar_Event");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.IdCandidate);

            entity.ToTable("Candidate");

            entity.Property(e => e.AreaOfActivity)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CandidateName).HasMaxLength(20);
            entity.Property(e => e.CandidateSecondName).HasMaxLength(20);
            entity.Property(e => e.CandidateSurname).HasMaxLength(20);
            entity.Property(e => e.Rezume).HasColumnType("text");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment);

            entity.ToTable("Comment");

            entity.Property(e => e.CommentText).HasColumnType("ntext");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");

            entity.HasOne(d => d.AuthorOfCommentNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorOfComment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Employee");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.CommentsNavigation)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Material");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment);

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("ntext");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Department_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee);

            entity.ToTable("Employee");

            entity.Property(e => e.Cabinet).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.Other).HasColumnType("text");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PhoneWork).HasMaxLength(20);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.SecondName).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent);

            entity.ToTable("Event");

            entity.Property(e => e.DateOfEvent).HasColumnType("datetime");
            entity.Property(e => e.EventDescription).HasColumnType("ntext");
            entity.Property(e => e.EventManagers).HasMaxLength(50);
            entity.Property(e => e.EventName).HasMaxLength(50);
            entity.Property(e => e.EventStatus).HasMaxLength(15);
            entity.Property(e => e.TypeOfClass).HasMaxLength(100);
            entity.Property(e => e.TypeOfEvent).HasMaxLength(50);
        });

        modelBuilder.Entity<EventMaterial>(entity =>
        {
            entity.HasKey(e => e.IdEventMaterial);

            entity.ToTable("EventMaterial");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.EventMaterials)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMaterial_Event");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.EventMaterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMaterial_Material");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.IdMaterial);

            entity.ToTable("Material");

            entity.Property(e => e.Author).HasMaxLength(20);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.DateApproval).HasColumnType("datetime");
            entity.Property(e => e.DateChanges).HasColumnType("datetime");
            entity.Property(e => e.Domain).HasMaxLength(50);
            entity.Property(e => e.MaterialName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.TypeOfMaterial).HasMaxLength(20);
        });

        modelBuilder.Entity<WorkingCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkingCalendar_pk");

            entity.ToTable("WorkingCalendar", tb => tb.HasComment("Список дней исключений в производственном календаре"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Идентификатор строки");
            entity.Property(e => e.ExceptionDate).HasComment("День-исключение");
            entity.Property(e => e.IsWorkingDay).HasComment("0 - будний день, но законодательно принят выходным");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
