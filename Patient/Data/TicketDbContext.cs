using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TicketService.Models;

namespace TicketService.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
    public class TicketDbContextFactory : IDesignTimeDbContextFactory<TicketDbContext>
    {
        public TicketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketDbContext>();

            // 💡 Make sure this matches the connection string in appsettings.json
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TicketDb;Trusted_Connection=True;");

            return new TicketDbContext(optionsBuilder.Options);
        }

    }
}
