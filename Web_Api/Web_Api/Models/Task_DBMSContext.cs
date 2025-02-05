﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web_Api.Models
{
    public partial class Task_DBMSContext : DbContext
    {
        public Task_DBMSContext()
        {
        }

        public Task_DBMSContext(DbContextOptions<Task_DBMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DeptMaxTop3Salary> DeptMaxTop3Salaries { get; set; } = null!;
        public virtual DbSet<DeptSalary> DeptSalaries { get; set; } = null!;
        public virtual DbSet<UsersDetail> UsersDetails { get; set; } = null!;
        public virtual DbSet<UsersProof> UsersProofs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2M6T9UV\\SQLEXPRESS;Initial Catalog=Task_DBMS;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeptMaxTop3Salary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Dept_MAX_TOP3_salary");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SALARY");
            });

            modelBuilder.Entity<DeptSalary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Dept_salary");

                entity.Property(e => e.DepartmentUserCount).HasColumnName("Department_User_Count");

                entity.Property(e => e.MaximumSalary)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Maximum_Salary");

                entity.Property(e => e.MinimumSalary)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Minimum_Salary");
            });

            modelBuilder.Entity<UsersDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users_De__1788CC4CCDF1CC4D");

                entity.ToTable("Users_Details");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate).HasColumnType("date");

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserProofStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UsersDetails)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users_Det__IsDel__2D27B809");

                entity.HasOne(d => d.UserProof)
                    .WithMany(p => p.UsersDetails)
                    .HasForeignKey(d => d.UserProofId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users_Det__UserP__2E1BDC42");
            });

            modelBuilder.Entity<UsersProof>(entity =>
            {
                entity.HasKey(e => e.UserProofId)
                    .HasName("PK__Users_Pr__483E81284B7D84F0");

                entity.ToTable("Users_Proof");

                entity.Property(e => e.ProofName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
