using MediatR;
using TicketService.Data;

namespace TicketService.Application
{
    public class MarkTicketAsPaidCommand : IRequest<bool>
    {
        public Guid TicketId { get; set; }
    }

    public class MarkTicketAsPaidHandler : IRequestHandler<MarkTicketAsPaidCommand, bool>
    {
        private readonly TicketDbContext _context;

        public MarkTicketAsPaidHandler(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(MarkTicketAsPaidCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            if (ticket == null || ticket.IsCancelled || ticket.IsPaid)
                return false;

            if (ticket.ExpiresAt < DateTime.UtcNow)
            {
                ticket.IsCancelled = true;
                await _context.SaveChangesAsync();
                return false;
            }

            ticket.IsPaid = true;
            ticket.PurchaseDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }

}