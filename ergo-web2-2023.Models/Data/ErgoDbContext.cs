using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ergo_web2_2023.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace ergo_web2_2023.Models.Data
{
    public partial class ErgoDbContext : DbContext
    {
        public ErgoDbContext()
        {
        }

        public ErgoDbContext(DbContextOptions<ErgoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Form> Forms { get; set; } = null!;
        public virtual DbSet<FormGroup> FormGroups { get; set; } = null!;
        public virtual DbSet<FormQuestion> FormQuestions { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; } = null!;
        public virtual DbSet<Subquestion> Subquestions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<FormGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<FormQuestion>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.QuestionId });

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_FormQuestions_Forms");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_FormQuestions_FormGroups");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.FormQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_FormQuestions_Questions");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Hint).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasKey(e => new { e.OptionId, e.QuestionId });

                entity.Property(e => e.OptionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Optiontext).IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_QuestionOptions_Questions");
            });

            modelBuilder.Entity<Subquestion>(entity =>
            {
                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.SubQuestionId).HasColumnName("SubQuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SubquestionQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Subquestions_Questions");

                entity.HasOne(d => d.SubQuestion)
                    .WithMany(p => p.SubquestionSubQuestions)
                    .HasForeignKey(d => d.SubQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subquestions_Questions1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
