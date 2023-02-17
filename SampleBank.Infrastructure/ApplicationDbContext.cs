using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleBank.Domain.Entities;

namespace SampleBank.Infrastructure
{
    /// <summary>
    /// DB context Class
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ErrorLog> Errors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(customer => customer.UserId);
                entity.Property(customer => customer.FirstName).IsRequired();
                entity.Property(customer => customer.LastName).IsRequired();
                entity.Property(customer => customer.UserId).IsRequired();
                entity.Property(customer => customer.Email).IsRequired();
                entity.Property(customer => customer.CustomerId).IsRequired();

            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(account => account.Id);
                entity.Property(account => account.AccountNumber).IsRequired();
                entity.Property(account => account.AccountBalance).IsRequired();
                entity.Property(account => account.UserId).IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(transaction => transaction.Id);
                entity.Property(transactionInfo => transactionInfo.Amount).IsRequired();
                entity.Property(transactionInfo => transactionInfo.SourceAccountId).IsRequired();
                entity.Property(transactionInfo => transactionInfo.DestinationAccountId).IsRequired();
                entity.Property(transactionInfo => transactionInfo.CreatedOn).IsRequired();

            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");
                entity.HasKey(error => error.Id);
            });
        }
    }
}