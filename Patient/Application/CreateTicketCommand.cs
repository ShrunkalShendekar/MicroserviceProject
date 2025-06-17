using MediatR;
using TicketService.Data;
using TicketService.Models;



namespace TicketService.Application
{
    public class CreateTicketCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public Guid TicketTypeId { get; set; }
        public string BuyerName { get; set; }
        public string Email { get; set; }
    }

    public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, Guid>
    {
        private readonly TicketDbContext _context;

        public CreateTicketHandler(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                EventId = request.EventId,
                TicketTypeId = request.TicketTypeId,
                BuyerName = request.BuyerName,
                Email = request.Email,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15), // reservation valid for 15 mins
                IsReserved = true,
                IsPaid = false,
                IsCancelled = false,
                ReservationCode = Guid.NewGuid().ToString().Substring(0, 8) // short reservation code
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
    }
        
}



