using MediatR;
using TicketService.Data;

namespace TicketService.Application
{
    public class CancelTicketReservationCommand : IRequest<bool>
    {
        public Guid TicketId { get; set; }
    }

    public class CancelTicketReservationHandler : IRequestHandler<CancelTicketReservationCommand, bool>
    {
        private readonly TicketDbContext _context;

        public CancelTicketReservationHandler(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CancelTicketReservationCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            if (ticket == null || ticket.IsCancelled || ticket.IsPaid)
                return false;

            ticket.IsCancelled = true;
            ticket.IsReserved = false;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
