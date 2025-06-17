using EventService.Model;
using EventService.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace EventService.Data
{
    public class EventDbContext:DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketType>()
                .HasOne(t => t.Event)
                .WithMany(e => e.TicketTypes)
                .HasForeignKey(t => t.EventId);
        }
    }
}
