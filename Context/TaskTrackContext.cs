using System;
using System.Collections.Generic;
using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Models;
using KyrsachAPI.Models.User;
using Microsoft.EntityFrameworkCore;


namespace KyrsachAPI.Context;

public partial class TaskTrackContext : DbContext
{
    public TaskTrackContext()
    {
    }

    public TaskTrackContext(DbContextOptions<TaskTrackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<MenuItemsAccess> MenuItemsAccesses { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }

    public virtual DbSet<TaskExecutor> TaskExecutors { get; set; }

    public virtual DbSet<TaskStep> TaskSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserActiveStatus> UserActiveStatuses { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public DbSet<ProductType> ProductTypes { get; set; }

    public DbSet<Brands> Brands { get; set; }

    //public DbSet<TaskStastusToday> TaskStastusToday { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TaskTrack;User Id=taskmonitor;Password=taskmonitor;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MenuItems", "snd");

            entity.Property(e => e.MenuItemId).ValueGeneratedOnAdd();
            entity.Property(e => e.MenuItemName).HasMaxLength(20);
        });

        modelBuilder.Entity<MenuItemsAccess>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MenuItemsAccess", "snd");

            entity.Property(e => e.AccessMenuId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity
                .ToTable("Task");

            entity.Property(e => e.TaskCloseDate).HasColumnType("datetime");
            entity.Property(e => e.TaskCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            entity.Property(e => e.TaskPlanExeTime).HasColumnType("datetime");
            entity.Property(e => e.TaskProductModel).HasMaxLength(200);
        });

        modelBuilder.Entity<TaskExecutor>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.TaskExecutorId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TaskStep>(entity =>
        {
            entity.Property(e => e.TaskStepCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TaskStepId).ValueGeneratedOnAdd();
            entity.Property(e => e.TaskStepText).HasMaxLength(300);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Users", "snd");

            entity.Property(e => e.UserActiveStatus).HasDefaultValue(true);
            entity.Property(e => e.UserCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserEmail).HasMaxLength(20);
            entity.Property(e => e.UserFirstName).HasMaxLength(15);
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.UserLastName).HasMaxLength(15);
            entity.Property(e => e.UserLogin).HasMaxLength(20);
            entity.Property(e => e.UserMiddleName).HasMaxLength(15);
            entity.Property(e => e.UserPassword).HasMaxLength(64);
        });

        modelBuilder.Entity<UserActiveStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserActiveStatuses", "snd");

            entity.Property(e => e.UserActStName).HasMaxLength(30);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRoles", "snd");

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName).HasMaxLength(15);
            entity.Property(e => e.RoleTextName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
