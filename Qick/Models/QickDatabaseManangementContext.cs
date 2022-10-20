﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Qick.Models
{
    public partial class QickDatabaseManangementContext : DbContext
    {
        public QickDatabaseManangementContext()
        {
        }

        public QickDatabaseManangementContext(DbContextOptions<QickDatabaseManangementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddmissionCampaign> AddmissionCampaigns { get; set; } = null!;
        public virtual DbSet<AddmissionNew> AddmissionNews { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<Attempt> Attempts { get; set; } = null!;
        public virtual DbSet<AttemptDetail> AttemptDetails { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Fqa> Fqas { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobMajor> JobMajors { get; set; } = null!;
        public virtual DbSet<JobMapping> JobMappings { get; set; } = null!;
        public virtual DbSet<Major> Majors { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionType> QuestionTypes { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<TestType> TestTypes { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;
        public virtual DbSet<UniversitySpecialization> UniversitySpecializations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DbCon");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddmissionCampaign>(entity =>
            {
                entity.ToTable("AddmissionCampaign");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.AddmissionCampaigns)
                    .HasForeignKey(d => d.UniId)
                    .HasConstraintName("FK_TblAddmissionCampaign_TblUniversity");
            });

            modelBuilder.Entity<AddmissionNew>(entity =>
            {
                entity.ToTable("AddmissionNew");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.AddmissionNews)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_TblAddmissionNew_TblAddmissionCampaign");

                entity.HasOne(d => d.UniSpec)
                    .WithMany(p => p.AddmissionNews)
                    .HasForeignKey(d => d.UniSpecId)
                    .HasConstraintName("FK_TblAddmissionNew_TblUniversitySpecialization");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Application");

                entity.Property(e => e.ApplyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Uni)
                    .WithMany()
                    .HasForeignKey(d => d.UniId)
                    .HasConstraintName("FK_TblApplication_TblUniversity");

                entity.HasOne(d => d.UniSpec)
                    .WithMany()
                    .HasForeignKey(d => d.UniSpecId)
                    .HasConstraintName("FK_TblApplication_TblUniversitySpecialization");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TblApplication_TblUser");
            });

            modelBuilder.Entity<Attempt>(entity =>
            {
                entity.ToTable("Attempt");

                entity.Property(e => e.AttemptDate).HasColumnType("datetime");

                entity.Property(e => e.Result).HasMaxLength(50);

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Attempts)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_TblAttempt_TblQuiz");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attempts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TblAttempt_TblUser");
            });

            modelBuilder.Entity<AttemptDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AttemptDetail");

                entity.HasOne(d => d.Attempt)
                    .WithMany()
                    .HasForeignKey(d => d.AttemptId)
                    .HasConstraintName("FK_TblAttemptDetail_TblAttempt");

                entity.HasOne(d => d.Option)
                    .WithMany()
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_TblAttemptDetail_TblOption");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_City_Province");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityId).HasColumnName("CItyId");

                entity.Property(e => e.DistrictName).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_District_City");
            });

            modelBuilder.Entity<Fqa>(entity =>
            {
                entity.ToTable("FQA");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Fqaanswer).HasColumnName("FQAAnswer");

                entity.Property(e => e.Fqacontent).HasColumnName("FQAContent");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.Fqas)
                    .HasForeignKey(d => d.UniId)
                    .HasConstraintName("FK_TblFQA_TblUniversity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Fqas)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TblFQA_TblUser");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");
            });

            modelBuilder.Entity<JobMajor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobMajor");

                entity.HasOne(d => d.Job)
                    .WithMany()
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobMajor_Job");

                entity.HasOne(d => d.Major)
                    .WithMany()
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobMajor_Major");
            });

            modelBuilder.Entity<JobMapping>(entity =>
            {
                entity.ToTable("JobMapping");

                entity.HasOne(d => d.Attempt)
                    .WithMany(p => p.JobMappings)
                    .HasForeignKey(d => d.AttemptId)
                    .HasConstraintName("FK_AttemptResult_Attempt");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobMappings)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_AttemptResult_Job");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("Option");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OptionType).HasMaxLength(50);

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_TblOption_TblQuestion");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProvinceName).HasMaxLength(50);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .HasConstraintName("FK_Question_QuestionType");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_TblQuestion_TblQuiz");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.ToTable("QuestionType");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Specializations)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_TblSpecialization_TblMajor");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK_TblQuiz_TblUser");

                entity.HasOne(d => d.QuizType)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.QuizTypeId)
                    .HasConstraintName("FK_TblQuiz_TblQuizType");
            });

            modelBuilder.Entity<TestType>(entity =>
            {
                entity.ToTable("TestType");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.ToTable("University");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vippack)
                    .HasMaxLength(10)
                    .HasColumnName("VIPPack")
                    .IsFixedLength();

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Universities)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_University_Ward");
            });

            modelBuilder.Entity<UniversitySpecialization>(entity =>
            {
                entity.ToTable("UniversitySpecialization");

                entity.Property(e => e.SpecId).HasMaxLength(50);

                entity.HasOne(d => d.Spec)
                    .WithMany(p => p.UniversitySpecializations)
                    .HasForeignKey(d => d.SpecId)
                    .HasConstraintName("FK_TblUniversitySpecialization_TblSpecialization");

                entity.HasOne(d => d.Uni)
                    .WithMany(p => p.UniversitySpecializations)
                    .HasForeignKey(d => d.UniId)
                    .HasConstraintName("FK_TblUniversitySpecialization_TblUniversity");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressNumber).HasMaxLength(100);

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CredentialId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SignUpDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_TblUser_TblUserRole");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_TblUser_TblUniversity");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_User_Ward");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("Ward");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.WardName).HasMaxLength(100);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Ward_District");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
