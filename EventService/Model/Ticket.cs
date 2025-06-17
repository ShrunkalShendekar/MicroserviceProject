namespace TicketService.Models
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Primary Key

        public Guid EventId { get; set; } // Foreign Key (from EventService)
        public Guid TicketTypeId { get; set; }           // FK to TicketType (VIP, General)

        public string BuyerName { get; set; }
        public string Email { get; set; }

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;  // When reservation was made
        public DateTime? ExpiresAt { get; set; }          // Optional: reservation timeout

        public DateTime? PurchaseDate { get; set; }       // When payment was made

        public bool IsReserved { get; set; } = true;      // Initially true when reserved
        public bool IsPaid { get; set; } = false;         // True only after payment
        public bool IsCancelled { get; set; } = false;    // If reservation is cancelled

        // Optionally, you can add
        public string ReservationCode { get; set; }
    }
}
