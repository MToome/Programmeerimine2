using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
