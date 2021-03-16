using Microsoft.EntityFrameworkCore;
using FinanceApp.Shared.Models;
using FinanceApp.Shared.Enums;

namespace FinanceApp.Server.Data {
    public class FinanceAppContext : DbContext
    {
        public FinanceAppContext (DbContextOptions<FinanceAppContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AccountVendor> AccountVendors { get; set; }

        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity => 
            {
                entity.Property(c => c.Status).HasDefaultValue(GenericStatus.Active);
                entity.ToTable("Category");
            });
            
            modelBuilder.Entity<AccountVendor>(entity => 
            {
                entity.Property(c => c.Status).HasDefaultValue(GenericStatus.Active);
                entity.ToTable("AccountVendor");
            });
            
            modelBuilder.Entity<Account>(entity => 
            {
                entity.Property(c => c.Status).HasDefaultValue(AccountStatus.Active);
                entity.ToTable("Account");
            });

            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}