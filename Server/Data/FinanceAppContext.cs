using Microsoft.EntityFrameworkCore;
using FinanceApp.Server.Models;

namespace FinanceApp.Server.Data {
    public class FinanceAppContext : DbContext
    {
        public FinanceAppContext (DbContextOptions<FinanceAppContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}