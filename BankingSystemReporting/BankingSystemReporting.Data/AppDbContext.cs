namespace BankingSystemReporting.Data
{
    using Microsoft.EntityFrameworkCore;

    using BankingSystemReporting.Models;

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
