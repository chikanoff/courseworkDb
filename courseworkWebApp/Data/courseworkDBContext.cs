using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using courseworkWebApp.Models;

namespace courseworkWebApp.Data
{
    public partial class courseworkDBContext : DbContext
    {
        public courseworkDBContext()
        {
        }

        public courseworkDBContext(DbContextOptions<courseworkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ConfirmationDocument> ConfirmationDocuments { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<ToiPolicy> ToiPolicies { get; set; }
        public virtual DbSet<TypeOfinsurance> TypesOfinsurance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-3DPM8BP\\SQLEXPRESS;Database=courseworkDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Commision).HasColumnName("commision");

                entity.Property(e => e.Contract)
                    .HasColumnName("contract")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IsWorker)
                    .IsRequired()
                    .HasColumnName("isWorker")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agentId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.PolicyId).HasColumnName("policyId");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Cases_Agents");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cases_Policies");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.SerialNumber)
                    .HasName("UQ__Clients__BED14FEE6A58A8C1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasColumnName("adress")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middleName")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Residence)
                    .IsRequired()
                    .HasColumnName("residence")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasColumnName("serialNumber")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConfirmationDocument>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CaseId).HasColumnName("caseId");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasColumnName("filePath")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UploadedAt)
                    .HasColumnName("uploadedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.ConfirmationDocuments)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_ConfirmationDocuments_Cases");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agentId");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Finish)
                    .HasColumnName("finish")
                    .HasColumnType("datetime");

                entity.Property(e => e.RegistredAt)
                    .HasColumnName("registredAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Policies_Agents");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Policies_Clients");
            });

            modelBuilder.Entity<ToiPolicy>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PolicyId).HasColumnName("policyId");

                entity.Property(e => e.ToiId).HasColumnName("toiId");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.ToiPolicies)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_ToiPolicies_Policies");

                entity.HasOne(d => d.Toi)
                    .WithMany(p => p.ToiPolicies)
                    .HasForeignKey(d => d.ToiId)
                    .HasConstraintName("FK_ToiPolicies_TypesOfinsurance");
            });

            modelBuilder.Entity<TypeOfinsurance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Payment)
                    .HasColumnName("payment")
                    .HasColumnType("money");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
