using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Boors.Entities;

namespace Boors.Models
{
    public partial class BourseContext : DbContext
    {
        public BourseContext()
        {
        }

        public BourseContext(DbContextOptions<BourseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Condition> Condition { get; set; }
        public virtual DbSet<ConditionOperator> ConditionOperator { get; set; }
        public virtual DbSet<Monitor> Monitor { get; set; }
        public virtual DbSet<MonitorDay> MonitorDay { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Operator> Operator { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionHourlyChange> TransactionHourlyChange { get; set; }
        public virtual DbSet<TransactionHourlyChangeStage> TransactionHourlyChangeStage { get; set; }
        public virtual DbSet<UniqueTransaction> UniqueTransaction { get; set; }
        public virtual DbSet<UniqueTransactionStage> UniqueTransactionStage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserMonitor> UserMonitor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Bourse;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Condition>(entity =>
            {
                entity.Property(e => e.FieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstChildId).HasColumnName("FirstChildID");

                entity.Property(e => e.PropName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondChildId).HasColumnName("SecondChildID");

                entity.Property(e => e.Variable1).HasMaxLength(50);

                entity.Property(e => e.Variable2).HasMaxLength(50);

                entity.HasOne(d => d.ConditionOperator)
                    .WithMany(p => p.Condition)
                    .HasForeignKey(d => d.ConditionOperatorId)
                    .HasConstraintName("FK_Condition_ConditionOperator");

                entity.HasOne(d => d.FirstChild)
                    .WithMany(p => p.InverseFirstChild)
                    .HasForeignKey(d => d.FirstChildId)
                    .HasConstraintName("FK_Condition_Condition");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Condition)
                    .HasForeignKey(d => d.OperationId)
                    .HasConstraintName("FK_Condition_Operation");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.Condition)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK_Condition_Operator");

                entity.HasOne(d => d.SecondChild)
                    .WithMany(p => p.InverseSecondChild)
                    .HasForeignKey(d => d.SecondChildId)
                    .HasConstraintName("FK_Condition_Condition1");
            });

            modelBuilder.Entity<ConditionOperator>(entity =>
            {
                entity.Property(e => e.ConditionOperatorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Monitor>(entity =>
            {
                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.Monitor)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Monitor_Condition");
            });

            modelBuilder.Entity<MonitorDay>(entity =>
            {
                entity.Property(e => e.DayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Monitor)
                    .WithMany(p => p.MonitorDay)
                    .HasForeignKey(d => d.MonitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonitorDay_Monitor");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.OperationFaName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.Property(e => e.OperatorFaName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.BourseCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Declaration).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Precedence).HasMaxLength(50);

                entity.Property(e => e.RequestCode).HasMaxLength(15);

                entity.Property(e => e.RootGroup).HasMaxLength(50);

                entity.Property(e => e.SellerBuyer).HasMaxLength(100);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TransactionHourlyChange>(entity =>
            {
                entity.Property(e => e.TransactionHourlyChangeId).HasColumnName("TransactionHourlyChangeID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.BourseCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TransactionHourlyChangeStage>(entity =>
            {
                entity.ToTable("TransactionHourlyChangeStage", "stg");

                entity.Property(e => e.TransactionHourlyChangeStageId).HasColumnName("TransactionHourlyChangeStageID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.BourseCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UniqueTransaction>(entity =>
            {
                entity.Property(e => e.UniqueTransactionId).HasColumnName("UniqueTransactionID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.BourseCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Counter).HasDefaultValueSql("((1))");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Declaration).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Precedence).HasMaxLength(50);

                entity.Property(e => e.RequestCode).HasMaxLength(15);

                entity.Property(e => e.RootGroup).HasMaxLength(50);

                entity.Property(e => e.SellerBuyer).HasMaxLength(100);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UniqueTransactionStage>(entity =>
            {
                entity.ToTable("UniqueTransactionStage", "stg");

                entity.Property(e => e.UniqueTransactionStageId).HasColumnName("UniqueTransactionStageID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.BourseCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Counter).HasDefaultValueSql("((1))");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Declaration).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Precedence).HasMaxLength(50);

                entity.Property(e => e.RequestCode).HasMaxLength(15);

                entity.Property(e => e.RootGroup).HasMaxLength(50);

                entity.Property(e => e.SellerBuyer).HasMaxLength(100);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserMonitor>(entity =>
            {
                entity.ToTable("User_Monitor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Monitor)
                    .WithMany(p => p.UserMonitor)
                    .HasForeignKey(d => d.MonitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Monitor_Monitor");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMonitor)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Monitor_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
