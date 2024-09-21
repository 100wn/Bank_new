using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIBank.Model;

public partial class DbBankContext : DbContext
{
    public DbBankContext()
    {
    }

    public DbBankContext(DbContextOptions<DbBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=db_bank", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.ClientId, "idx_ClientId");

            entity.Property(e => e.Balance).HasPrecision(15, 2);

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_ibfk_1");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PRIMARY");

            entity.ToTable("card");

            entity.HasIndex(e => e.AccountId, "AccountId");

            entity.HasIndex(e => e.CardNumber, "CardNumber").IsUnique();

            entity.HasIndex(e => e.ClientId, "ClientId");

            entity.Property(e => e.CardNumber).HasMaxLength(16);
            entity.Property(e => e.CvvCvcCod)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("CVV_CVC_cod");
            entity.Property(e => e.MonthlyCardMaintenance).HasMaxLength(30);
            entity.Property(e => e.PaymentSystem)
                .HasMaxLength(20)
                .HasColumnName("Payment_system");
            entity.Property(e => e.PinCod)
                .HasMaxLength(4)
                .IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Cards)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_ibfk_2");

            entity.HasOne(d => d.Client).WithMany(p => p.Cards)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.IdRole).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(80);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity.ToTable("transactions");

            entity.HasIndex(e => e.AccountId, "idx_TransactionAccountId");

            entity.HasIndex(e => e.ClientId, "idx_TransactionClientId");

            entity.HasIndex(e => e.SenderCardId, "transactions_ibfk_2");

            entity.Property(e => e.DateTimeTransaction)
                .HasColumnType("datetime")
                .HasColumnName("Date_time_transaction");
            entity.Property(e => e.InfoCommission).HasMaxLength(20);
            entity.Property(e => e.OperationDescription)
                .HasMaxLength(70)
                .HasColumnName("Operation_description");
            entity.Property(e => e.OptionAmountTransaction).HasMaxLength(20);
            entity.Property(e => e.RecipientCardNumber).HasMaxLength(16);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_ibfk_3");

            entity.HasOne(d => d.Client).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_ibfk_1");

            entity.HasOne(d => d.SenderCard).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SenderCardId)
                .HasConstraintName("transactions_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRoleId, "UserRoleId");

            entity.HasIndex(e => e.UserEmail, "idx_UserEmail");

            entity.HasIndex(e => e.UserNumberPhone, "idx_UserNumberPhone");

            entity.Property(e => e.UserEmail).HasMaxLength(50);
            entity.Property(e => e.UserFio)
                .HasMaxLength(40)
                .HasColumnName("UserFIO");
            entity.Property(e => e.UserNumberPhone).HasMaxLength(18);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(64)
                .IsFixedLength();

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
