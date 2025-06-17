using EventService.NewFolder;

namespace EventService.Model
{
    public class TicketType
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }   // Foreign key
        public string Name { get; set; }    // e.g., VIP, General
        public decimal Price { get; set; }
        public int Quantity { get; set; }   // Total tickets of this type

        public Event Event { get; set; }    // Navigation property
    }
}
