﻿using System;
using System.Collections.Generic;
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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserActiveStatus> UserActiveStatuses { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TaskTrack;User Id=taskmonitor;Password=taskmonitor;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.UserPassword).HasMaxLength(32);
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
