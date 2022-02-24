using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BankApp.Models
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; } = null!;
    }
}